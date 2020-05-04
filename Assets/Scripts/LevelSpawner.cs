using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LevelSpawner : MonoBehaviour
{
    public event UnityAction<float> TimePassed;
    [SerializeField] private BomberMover _bomberTemplate;
    [SerializeField] private SuiciderMover _suiciderTemplate;
    [SerializeField] private HarvesterMover _harvesterTemplate;
    [SerializeField] private Flock _flockTemplate;
    [SerializeField] private TransportMover _transportTemplate;
    [SerializeField] private Boss _boss1Template;
    [SerializeField] private Boss _boss2Template;
    [SerializeField] private Boss _boss3Template;
    [SerializeField] private TMP_Text _detectorText;    
    [SerializeField] private Level[] _levels;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Player _player;
    [SerializeField] private Station _station;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioClip _musicBoss;
    private Level _level;
    private List<GameObject> _currentEnemies = new List<GameObject>();
    private int _numRound = 0;
    private float _timeForNext = 0;
    private float _roundTimeTotal = 0;
    private float _roundTime = 0;
    private Round _currentRound;
    private string[] EnemyNames = { "элиминаторов", "бомберов", "взрывателей", "транспортов", "харвестеров" };

    private void Start()
    {
        _level = _levels[SaveSystem.Instance.GetPlayerData().CurrentLevel];
        if (_level.BossMusic)
        {
            _musicSource.clip = _musicBoss;
            _musicSource.Play();
        }
        /*
        for (int i = 0; i < _level.round.Length; i++)
        {
            _roundTimeTotal += _level.round[i].TimeToNextWave;
        }*/
        _roundTimeTotal = _level.TotalTime;
        _timeForNext = _level.round[_numRound].TimeToNextWave;
        _currentRound = _level.round[_numRound];
        Spawn();
    }

    private void Update()
    {
        if (_currentEnemies.Count == 0)
        {
            _timeForNext -= Time.deltaTime;
            _roundTime += Time.deltaTime;
        }
        if (_timeForNext <= 0 && _currentEnemies.Count == 0)
        {
            _numRound++;
            if (_numRound < _level.round.Length)
            {
                _timeForNext = _level.round[_numRound].TimeToNextWave;
                _currentRound = _level.round[_numRound];
                Spawn();
            }
        }

        TimePassed?.Invoke(_roundTime / _roundTimeTotal);
    }

    private void Spawn()
    {
        switch (_currentRound.WaveType)
        {
            case Wave.Eliminator:
                CreateFlock();
                ShowDetectorInfo(EnemyNames[(int)_currentRound.WaveType]);
                break;
            case Wave.Bomber:
                SpawnEnemy(_bomberTemplate.gameObject, true);
                break;
            case Wave.Suicider:
                SpawnEnemy(_suiciderTemplate.gameObject, true);
                break;
            case Wave.Transport:
                SpawnEnemy(_transportTemplate.gameObject, true);
                break;
            case Wave.Harvester:
                SetHarvesterTarget(SpawnEnemy(_harvesterTemplate.gameObject, false));
                break;
            case Wave.Boss1:
                //Boss1Spawn();
                BossSpawn(_boss1Template.gameObject);
                break;
            case Wave.Boss2:
                BossSpawn(_boss2Template.gameObject);
                break;
            case Wave.Boss3:
                BossSpawn(_boss3Template.gameObject);
                break;
            case Wave.None:
                break;
            case Wave.Win:
                Win();
                break;
        }
    }

    private void BossSpawn(GameObject bossTemplate)
    {
        GameObject boss = Instantiate(bossTemplate, _spawnPoints[_currentRound.NumberSpawner].position, Quaternion.identity);
        Component[] shooters = boss.GetComponentsInChildren(typeof(Shooter));

        foreach (Shooter shooter in shooters)
        {
            shooter.SetTargets(_player, _station);
        }
        //boss.GetComponent<Boss_1Shooter>().SetTargets(_player, _station);
        boss.GetComponent<EnemyMover>().SetTarget(_station.transform);
        ShowDetectorInfo("", false);
        if (_currentRound.IsNextWaveAfterDestroyCurrent)
        {
            SetEnemyToList(boss.gameObject);
        }
    }

    private void Boss1Spawn()
    {
        Boss boss = Instantiate(_boss1Template, _spawnPoints[_currentRound.NumberSpawner].position, Quaternion.identity);
        Component[] shooters = boss.GetComponentsInChildren(typeof(Shooter));

        foreach (Shooter shooter in shooters)
        {
            shooter.SetTargets(_player, _station);
        }
        //boss.GetComponent<Boss_1Shooter>().SetTargets(_player, _station);
        boss.GetComponent<EnemyMover>().SetTarget(_station.transform);
        ShowDetectorInfo("", false);
        if (_currentRound.IsNextWaveAfterDestroyCurrent)
        {
            SetEnemyToList(boss.gameObject);
        }
    }

    private void Win()
    {
        SaveSystem.Instance.PassLevel();
        SaveSystem.Instance.SaveData();
        SceneManager.LoadScene("PassedScene");
        _player.SetImmortal();
        _station.SetImmortal();
    }

    private List<GameObject> SpawnEnemy(GameObject enemy, bool isNeenSetTarget)
    {
        List<GameObject> enemies = new List<GameObject>();
        for (int i = 0; i < _currentRound.Count; i++)
        {
            var newEnemy = Instantiate(enemy, _spawnPoints[_currentRound.NumberSpawner].position + (Vector3)Random.insideUnitCircle * 5, Quaternion.identity);
            if (isNeenSetTarget)
            {
                newEnemy.GetComponent<Shooter>().SetTargets(_player, _station);
                newEnemy.GetComponent<EnemyMover>().SetTarget(_station.transform);
            }
            enemies.Add(newEnemy);
        }
        ShowDetectorInfo(EnemyNames[(int)_currentRound.WaveType]);
        if (_currentRound.IsNextWaveAfterDestroyCurrent)
        {
            SetEnemiesToList(enemies);
        }
        return enemies;
    }

    private void SetEnemiesToList(List<GameObject> enemies)
    {
        _currentEnemies = enemies;
        foreach (var enemy in _currentEnemies)
        {
            if (enemy.TryGetComponent(out Destroyable destroyable))
            {
                destroyable.Dead += RemoveFromEnemyList;
            } 
        }
    }

    private void SetEnemyToList(GameObject enemy)
    {
        _currentEnemies.Add(enemy);
        if (enemy.TryGetComponent(out Destroyable destroyable))
        {
            destroyable.Dead += RemoveFromEnemyList;
        }
    }

    private void SetEnemyFlockToList(GameObject enemyFlock)
    {
        _currentEnemies.Add(enemyFlock);
        foreach (var enemy in _currentEnemies)
        {
            if (enemy.TryGetComponent(out Flock flock))
            {
                flock.Dead += RemoveFromEnemyList;
            }
        }
    }

    private void RemoveFromEnemyList(GameObject enemy)
    {
        _currentEnemies.Remove(enemy);
        if (enemy.TryGetComponent(out Destroyable destroyable))
        {
            destroyable.Dead -= RemoveFromEnemyList;
        }
        else if (enemy.TryGetComponent(out Flock flock))
        {
            flock.Dead -= RemoveFromEnemyList;
        }
    }

    private void SetDestinationTarget(List<GameObject> enemies, Transform target)
    {
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<EnemyMover>().SetTarget(target);
        }
    }

    private void SetHarvesterTarget(List<GameObject> enemies)
    {
        int index = _currentRound.NumberSpawner + 4;
        if (index > 7)
            index -= 8;
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<HarvesterMover>().SetTarget(_spawnPoints[index]);
        }
    }

    private void CreateFlock()
    {
        Flock flock = Instantiate(_flockTemplate, _spawnPoints[_currentRound.NumberSpawner].position + (Vector3)Random.insideUnitCircle * 5, Quaternion.identity);
        flock.SetTarget(_player, _station);
        flock.SetAgentCount(_currentRound.Count);
        if (_currentRound.IsNextWaveAfterDestroyCurrent)
        {
            SetEnemyFlockToList(flock.gameObject);
        }
    }

    private void ShowDetectorInfo(string text, bool isStandardEnemy = true)
    {
        if (isStandardEnemy)
        {
            _detectorText.text = "Обнаружена группа " + text;
        }
        else
        {
            _detectorText.text = "Обнаружен массивный объект";
        }
        _audioSource.Play();
        StartCoroutine(ShowDetectorInfoCoroutine());
    }


    private IEnumerator ShowDetectorInfoCoroutine()
    {
        Color color = _detectorText.color;
        for (int i = 0; i < 10; i++)
        {
            color.a = 0.1f * i;
            _detectorText.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < 10; i++)
        {
            color.a = 1 - 0.1f * i;
            _detectorText.color = color;
            yield return null;
        }
        color.a = 0;
        _detectorText.color = color;

    }

}
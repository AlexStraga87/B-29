using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Levels/Level")]
public class Level : ScriptableObject
{
    [SerializeField] private Round[] round;

    [SerializeField] public int TotalTime;
    [SerializeField] public int MaxMoney;
    [SerializeField] private bool _isBossMusic;

    private void OnValidate()
    {
        int[] prices = { 30, 50, 20, 80, 200, 1000, 1200, 1200, 0, 0 };
        TotalTime = 0;
        MaxMoney = 0;
        for (int i = 0; i < round.Length; i++)
        {
            TotalTime += round[i].TimeToNextWave;
            MaxMoney += round[i].Count * prices[(int)round[i].WaveType];
            if (round[i].NumberSpawner > 7)
                round[i].NumberSpawner = Random.Range(0, 8);
        }
    }

    public bool IsBossMusic()
    {
        return _isBossMusic;
    }

    public Round[] GetRounds()
    {
        return round;
    }


}

[System.Serializable]
public struct Round
{
    public Wave WaveType;
    public int Count;
    public int TimeToNextWave;
    public int NumberSpawner;
    public bool IsNextWaveAfterDestroyCurrent;
}

[System.Serializable]
public enum Wave {Eliminator, Bomber, Suicider, Transport, Harvester, Boss1, Boss2, Boss3, None, Win }
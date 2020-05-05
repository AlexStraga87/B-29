using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerDataLoader))]
public class PlayerMover: MonoBehaviour
{
    public float _maxSpeed = 10f;
    [SerializeField] private PlayerDataLoader _playerDataLoader;
    [SerializeField] private GameObject _aimMouse;
    [SerializeField] protected AudioSource _audioSource;
    

    private Rigidbody2D _rigidBody2D;
    private Animator _animator;    
    private bool _isMoving;

    private void Start ()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected void Upgrades()
    {
        PlayerData playerData = _playerDataLoader.GetPlayerData();
        _maxSpeed += playerData.Upgrades[(int)UpgradesList.ShipSpeed] * 2.5f;
    }

    private void Update()
    {
        RotateToMouse();
        _isMoving = false;
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidBody2D.AddForce(transform.up * 4000 * Time.fixedDeltaTime);
            _isMoving = true;
            if (_audioSource.isPlaying == false)
                _audioSource.Play();
        }

        if (Input.GetMouseButton(1))
        {
            _rigidBody2D.velocity *= 0.90f;

        }


        if (_rigidBody2D.velocity.magnitude > _maxSpeed)
        {
            _rigidBody2D.velocity = Vector3.ClampMagnitude(_rigidBody2D.velocity, _maxSpeed);
        }

        _animator.SetBool("isMove", _isMoving);
        CheckBorders();
    }

    private void CheckBorders()
    {
        if (Mathf.Abs(transform.position.x) > 80 || Mathf.Abs(transform.position.y) > 80)
        {
            _rigidBody2D.AddForce(-transform.position.normalized * 400 );
        }
    }

    private void RotateToMouse()
    {
        Vector3 screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        screenPoint.z = -1;
        _aimMouse.transform.position = screenPoint;
        Vector2 direction = screenPoint - transform.position;
        var angle = Vector2.SignedAngle(Vector2.up, direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 10 * Time.deltaTime);
    }
}

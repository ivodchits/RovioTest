using System;
using System.Collections.Generic;
using Containers;
using Settings;
using Systems;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [SerializeField] PlayerSettings _playerSettings;
    [SerializeField] PlayerInputSystem _inputSystem;
    [SerializeField] PlayerSystem _playerSystem;
    [SerializeField] CameraContainer _cameraContainer;
    [SerializeField] BulletController _bulletPrefab;
    [SerializeField] Transform _gunPivot;
    [SerializeField] Transform _bulletSpawnPoint;
    [SerializeField] int _bulletPoolSize = 20;
    [SerializeField] float _bulletSpeed = 16;

    void Start()
    {
        _characterController = _playerSystem.CharacterController;

        _bulletParent = new GameObject("Bullets").transform;
        for (int i = 0; i < _bulletPoolSize; i++)
        {
            var bullet = Instantiate(_bulletPrefab, _bulletParent);
            bullet.GameObject.SetActive(false);
            _bullets.Add(bullet);
        }

        _inputSystem.GameplayInput.Shoot += OnShoot;
    }

    void Update()
    {
        var lookDirection = _characterController.LookDirection;
        if (lookDirection.sqrMagnitude > Mathf.Epsilon)
        {
            _gunPivot.rotation = GetLookRotation(lookDirection, false);
        }
    }

    Quaternion GetLookRotation(Vector2 lookDirection, bool flip)
    {
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        if (flip || _characterController.Transform.localScale.x > 0)
            angle += 180;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnShoot()
    {
        if (!_characterController.Sliding && Time.time - _lastShotTime > _playerSettings.ShootingCooldown)
        {
            var bullet = GetBullet();
            bullet.Shoot(_bulletSpawnPoint.position, GetLookRotation(_characterController.LookDirection, true), _bulletSpeed);
            _lastShotTime = Time.time;
            _cameraContainer.CameraController.Shake();
        }
    }

    BulletController GetBullet()
    {
        foreach (var bullet in _bullets)
        {
            if (!bullet.GameObject.activeSelf)
                return bullet;
        }

        var newBullet = Instantiate(_bulletPrefab, _bulletParent);
        _bullets.Add(newBullet);
        return newBullet;
    }

    private void OnDestroy()
    {
        Destroy(_bulletParent.gameObject);
        _inputSystem.GameplayInput.Shoot -= OnShoot;
    }

    readonly List<BulletController> _bullets = new();
    PlayerCharacterController _characterController;
    Transform _bulletParent;
    float _lastShotTime = float.MinValue;
}
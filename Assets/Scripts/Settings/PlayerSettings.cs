using System;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "Settings/Player")]
    public class PlayerSettings : ScriptableObject
    {
        [SerializeField] MovementSettings _movement;
        [SerializeField] CameraSettings _camera;
        [SerializeField] float _shootingCooldown = 0.25f;
        [SerializeField] float _shootingInputThreshold = 0.9f;

        public MovementSettings Movement => _movement;
        public CameraSettings Camera => _camera;
        public float ShootingCooldown => _shootingCooldown;
        public float ShootingInputThreshold => _shootingInputThreshold;

        [Serializable]
        public class MovementSettings
        {
            [SerializeField] float _moveSpeed = 8f;
            [SerializeField] float _slideInitialSpeed = 12f;
            [SerializeField] float _acceleration = 0.5f;
            [SerializeField] float _deceleration = 0.5f;
            [SerializeField] float _slideDuration = 1f;
            [SerializeField] AnimationCurve _slideSpeedCurve;

            public float MoveSpeed => _moveSpeed;
            public float SlideInitialSpeed => _slideInitialSpeed;
            public float Acceleration => _acceleration;
            public float Deceleration => _deceleration;
            public float SlideDuration => _slideDuration;

            public float EvaluateSlideSpeed(float t) => _slideSpeedCurve.Evaluate(t);
        }

        [Serializable]
        public class CameraSettings
        {
            [SerializeField] float _camDistance = 5f;
            [SerializeField] float _maxCamAmplitude = 1.5f;
            [SerializeField] float _maxCamFrequency = 1f;
            [SerializeField] float _camShakeDuration = 0.2f;

            public float CamDistance => _camDistance;
            public float MaxCamAmplitude => _maxCamAmplitude;
            public float MaxCamFrequency => _maxCamFrequency;
            public float CamShakeDuration => _camShakeDuration;
        }
    }
}
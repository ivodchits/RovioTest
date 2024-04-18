using System;
using Settings;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Systems
{
    [CreateAssetMenu(fileName = "InputSystem", menuName = "Systems/InputSystem")]
    public class PlayerInputSystem : ScriptableObject, IInitializable, ITickable, IDisposable
    {
        [Inject] PlayerSettings _playerSettings;

        public IGameplayInput GameplayInput => _gameplayInput;

        public void Initialize()
        {
            _gameControls = new GameControls();
            _gameplayInput = new GameplayInputImpl(_gameControls.GameplayActionMap.Dodge);
            _gameControls.Enable();
        }

        public void Tick()
        {
            _gameplayInput.Movement = _gameControls.GameplayActionMap.Move.ReadValue<Vector2>();
            _gameplayInput.Look = _gameControls.GameplayActionMap.Look.ReadValue<Vector2>();
            if (_gameplayInput.Look.sqrMagnitude >= _playerSettings.ShootingInputThreshold)
                _gameplayInput.InvokeShoot();
        }

        public void Dispose()
        {
            _gameControls?.Dispose();
        }

        private class GameplayInputImpl : IGameplayInput
        {
            public Vector2 Movement { get; set; }
            public Vector2 Look { get; set; }
            public InputAction Dodge { get; }
            public event Action Shoot = delegate { };

            public GameplayInputImpl(InputAction dodge)
            {
                Dodge = dodge;
            }

            public void InvokeShoot() => Shoot();
        }

        GameControls _gameControls;
        GameplayInputImpl _gameplayInput;
    }

    public interface IGameplayInput
    {
        Vector2 Movement { get; }
        Vector2 Look { get; }
        InputAction Dodge { get; }
        event Action Shoot;
    }
}
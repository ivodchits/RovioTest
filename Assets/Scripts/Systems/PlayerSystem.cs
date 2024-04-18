using UnityEngine;
using Zenject;

namespace Systems
{
    [CreateAssetMenu(fileName = "PlayerSystem", menuName = "Systems/PlayerSystem")]
    public class PlayerSystem : ScriptableObject, IInitializable
    {
        [SerializeField] PlayerCharacterController _playerCharacterPrefab;
        [SerializeField] Vector3 _spawnPosition;

        public PlayerCharacterController CharacterController { get; private set; }

        public void Initialize()
        {
            CharacterController = Instantiate(_playerCharacterPrefab, _spawnPosition, Quaternion.identity);
        }
    }
}

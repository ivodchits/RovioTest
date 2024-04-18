using Settings;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    [SerializeField] PlayerSettings _playerSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(_playerSettings);
    }
}
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SystemsInstaller", menuName = "Installers/SystemsInstaller")]
public class SystemsInstaller : ScriptableObjectInstaller<SystemsInstaller>
{
    [SerializeField] ScriptableObject[] _systems;

    public override void InstallBindings()
    {
        foreach (var system in _systems)
        {
            Container.Inject(system);
            Container.BindInterfacesAndSelfTo(system.GetType()).FromInstance(system).AsSingle();
        }
    }
}
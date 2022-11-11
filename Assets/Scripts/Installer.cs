using Gauge;
using Zenject;

public class Installer : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Presenter>().FromNew().AsCached().NonLazy();
        Container.Bind<Model>().FromNew().AsCached();
    }
}
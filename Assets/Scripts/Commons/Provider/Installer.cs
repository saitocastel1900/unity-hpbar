using Gauge;
using Zenject;

namespace DI
{
    /// <summary>
    /// 
    /// </summary>
    public class Installer : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<HpBarPresenter>().FromNew().AsCached().NonLazy();
            Container.Bind<HpBarModel>().FromNew().AsCached();
        }
    }
}
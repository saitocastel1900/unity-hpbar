using HpBar;
using Zenject;

namespace DI
{
    /// <summary>
    /// HPオブジェクトの依存性注入
    /// </summary>
    public class HpBarInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            //HpPresenterを最初に生成して、注入
            Container.Bind(typeof(HpBarPresenter),typeof(IInitializable))
                .To<HpBarPresenter>()
                .AsCached()
                .NonLazy();
            
            //HpModelを生成して、注入
            Container.Bind<HpBarModel>()
                .AsCached()
                .WithArguments(0,0.0f);
        }
    }
}
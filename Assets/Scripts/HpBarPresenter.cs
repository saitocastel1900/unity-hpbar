using System;
using UniRx;
using UnityEngine;

namespace Gauge
{
    /// <summary>
    /// 
    /// </summary>
    public class HpBarPresenter : IDisposable
    {
        //view
        private HpBarView _view;
        //model
        private HpBarModel _model;
        
       CompositeDisposable disposables = new CompositeDisposable();
       
       public HpBarPresenter(HpBarModel model, HpBarView view)
       {
           Debug.Log("コンストラクタ発動");
           _model = model;
           _view = view;

           _view.Initialized();
           Bind();
           SetEvent();
       }

       private void Bind()
       {
           //view=>model
           _view.ObservableClickButton()
               .Subscribe(_ => _model.UpdateHp(),
                   ex => Debug.LogError("OnError!"),
                   () => Debug.Log("")).AddTo(disposables);

           //model=>view
           _model.HpProp
               .Subscribe(
                   _view.UpdateCounter,
                   ex => Debug.LogError("OnError!"),
                   () => Debug.Log("OnCompleted!")).AddTo(disposables);

           _model.HpBarAmountProp
               .Subscribe(_view.BarAnimation,
                   ex => Debug.LogError("OnError!"),
                   () => Debug.Log("OnCompleted!"))
               .AddTo(disposables);
        }

        private void SetEvent()
        {
            _model.OnCallback += _view.UnInteractiveClick;
        }
        
        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}
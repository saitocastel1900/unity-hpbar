using System;
using UniRx;
using UnityEngine;

namespace Gauge
{
    public class Presenter : IDisposable
    {
        //view
        private View _view;
        //model
        private Model _model;
        
       CompositeDisposable disposables = new CompositeDisposable();
       
       public Presenter(Model model, View view)
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
                .Select(_ => +1)
                .Subscribe(
                    value => _model.UpdateCount(_model.Value.Value + (int) value),
                    ex => Debug.LogError("OnError!"),
                    () => Debug.Log("")).AddTo(disposables);

            //model=>view
            _model.Value
                .Subscribe(x =>
                    {
                        _view.UpdateText(x);
                        _view.GaugeValue = x;
                        _view.GaugeAnimation();
                    },
                    ex => Debug.LogError("OnError!"),
                    () => Debug.Log("OnCompleted!")).AddTo(disposables);
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
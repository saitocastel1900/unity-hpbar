using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace HpBar
{
    /// <summary>
    /// HPのPresenter
    /// </summary>
    public class HpBarPresenter : IInitializable,IDisposable
    {
        /// <summary>
        /// View
        /// </summary>
        private HpBarView _view;
        
        /// <summary>
        /// Model
        /// </summary>
        private HpBarModel _model;
        
        //購読を解除するためのCompositeDisposable
       CompositeDisposable disposables = new CompositeDisposable();
       
       /// <summary>
       /// コンストラクタ
       /// </summary>
       public HpBarPresenter(HpBarModel model, HpBarView view)
       {
           Debug.Log("コンストラクタ発動");
           _model = model;
           _view = view;
       }
       
       /// <summary>
       /// Initialize
       /// </summary>
       public void Initialize()
       {
           _view.Initialize();
           Bind();
           SetEvent();
       }

       /// <summary>
       /// Bind
       /// </summary>
       private void Bind()
       {
           //HPが変化したら、カウンターを描画する
           _model.HpProp
               .Subscribe(
                   _view.SetCounter,
                   ex => Debug.LogError("OnError!"),
                   () => Debug.Log("OnCompleted!")).AddTo(disposables);

           //バー画像のFillAmountの数値が変化したら、
           _model.HpBarFillAmountProp
               .Subscribe(_view.BarAnimation,
                   ex => Debug.LogError("OnError!"),
                   () => Debug.Log("OnCompleted!"))
               .AddTo(disposables);
        }

       /// <summary>
       /// イベント設定
       /// </summary>
       private void SetEvent()
        {
            //ボタンを押せなくさせるイベントを追加
            _model.OnCallback += _view.UnInteractiveClick;
            
            //ボタンが押されたらHPを増加させる
            _view.ObservableClickButton()
                .Subscribe(_ => _model.UpdateValue(),
                    ex => Debug.LogError("OnError!"),
                    () => Debug.Log("")).AddTo(disposables);
        }
        
        public void Dispose()
        {
            disposables.Dispose();
        }
    }
}
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Extensions;
using TMPro;
using Utility;

namespace Gauge
{
    /// <summary>
    /// 
    /// </summary>
    public class HpBarView : MonoBehaviour
    {
        //HPバーを動かすボタン
        [SerializeField] private Button _button;

        //HPバー
        [SerializeField] private Image _barImage;
        [SerializeField] private float _barAnimationDuration = 0.2f;

        /// <summary>
        /// クリックのObservableを返す
        /// </summary>
        public IObservable<Unit> ObservableClickButton()
        {
            return _button.OnClickAsObservable();
        }

        /// <summary>
        /// ボタンをクリックできないようにする
        /// </summary>
        public void UnInteractiveClick()
        {
            _button.GetComponent<Button>().interactable = false;
        }

        private Tweener _tweener;

        /// <summary>
        /// 
        /// </summary>
        public void BarAnimation(float _barValue)
        {
            _tweener = HpBarAnimationUtility.FillAmountTween(_barImage, _barValue, _barAnimationDuration)
                .OnComplete(() =>_tweener.KillIfNotNull(true)).SetLink(this.gameObject);
        }
        
        //HPカウンター
        [SerializeField] private TextMeshProUGUI _counterText;
        
        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialized()
        {
            UpdateCounter(0);
        }
        
        /// <summary>
        /// 
        /// </summary>
        public void UpdateCounter(int counter)
        {
            _counterText.text = counter.ToString("#,0") + "/10";
        }
    }
}
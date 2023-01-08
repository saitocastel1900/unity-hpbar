using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Extensions;
using TMPro;
using Utility;

namespace HpBar
{
    /// <summary>
    /// HPバーのView
    /// </summary>
    public class HpBarView : MonoBehaviour
    {
        /// <summary>
        /// HPバーを動かすボタン
        /// </summary>
        [SerializeField] private Button _button;

        /// <summary>
        /// HPバーの画像
        /// </summary>
        [SerializeField] private Image _barImage;
        
        /// <summary>
        /// HPバーアニメーションの間隔
        /// </summary>
        [SerializeField] private float _barAnimationDuration = 0.2f;

        /// <summary>
        /// HPカウンター
        /// </summary>
        [SerializeField] private TextMeshProUGUI _counterText;
        
        /// <summary>
        /// HPバーアニメーションで使うtweener
        /// </summary>
        private Tweener _tweener;

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            SetCounter(0);
            _tweener = null;
        }
        
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
        
        /// <summary>
        /// HPバーの増加アニメーション
        /// </summary>
        public void BarAnimation(float _barValue)
        {
            _tweener = HpBarAnimationUtility.FillAmountTweener(_barImage, _barValue, _barAnimationDuration)
                .OnComplete(() =>_tweener.KillIfNotNull(true)).SetLink(this.gameObject);
        }

        /// <summary>
        /// カウンターの数字を設定
        /// </summary>
        public void SetCounter(int counter)
        {
            _counterText.text = counter.ToString("#,0") + "/10";
        }
    }
}
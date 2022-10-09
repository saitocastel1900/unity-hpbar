using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Gauge
{
    public class View : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Button _button;

        public void UpdateText(int value)
        {
            _text.text = value.ToString("#,0") + "/10";
        }

        //物によってはテキストにアニメーションを入れてもいいかも
        /*
        private void TextAnimation(string text)
        {
            if (_tweener != null)
            {
                _tweener.Kill();
                _tweener = null;
            }
            DOTween.To(() => currentValue, (x) => currentValue = x, endValue:value, duration)
                            .OnUpdate(() => _text.text = currentValue.ToString("#,0")).SetEase(Ease.OutCubic);
        
        }
        */

        public IObservable<Unit> ObserbableClickButton()
        {
            return _button.OnClickAsObservable();
        }

        public void UninteractiveClick()
        {
            _button.GetComponent<Button>().interactable=false;
        }

        #region GaugeAnimaion

        [SerializeField] private Image _gaugeImage;
        
        [SerializeField] private float _gaugeValue;

        public float GaugeValue
        {
            get => _gaugeValue;
            set
            {
                _gaugeValue = Mathf.Clamp(value * 0.1f, 0.0f, 1.0f);
            }
        }
        
        [SerializeField] private float duration = 0.2f;
        private Tweener _tweener=null;
        public void GaugeAnimation()
        {
            if (_tweener != null)
            {
                _tweener.Kill();
                _tweener = null;
            }

            _tweener = _gaugeImage.DOFillAmount(GaugeValue, duration)
                .OnComplete(() => Debug.Log("アニメーション修了"))
                .SetEase(Ease.OutCubic);
        }
    }

    #endregion
}
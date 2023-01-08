using System;
using Const;
using UniRx;
using UnityEngine;

namespace HpBar
{
    /// <summary>
    /// HPのModel
    /// </summary>
    public class HpBarModel
    {
        /// <summary>
        /// HP
        /// </summary>
        public IReadOnlyReactiveProperty<int> HpProp => _hpProp;
        private IntReactiveProperty _hpProp;

        /// <summary>
        /// HPバーの充填値
        /// </summary>
        public IReadOnlyReactiveProperty<float> HpBarFillAmountProp => _hpBarFillAmountProp;
        private FloatReactiveProperty _hpBarFillAmountProp;
        
        /// <summary>
        /// HPが最大値の時に呼ばれる
        /// </summary>
        public event Action OnCallback;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HpBarModel(int hp=0,float fillAmount=0.0f)
        {
            OnCallback = null;
            
            _hpProp=new IntReactiveProperty(hp);
            _hpBarFillAmountProp=new FloatReactiveProperty(fillAmount);
        }

        /// <summary>
        /// HPと充填値を設定
        /// </summary>
        public void UpdateValue()
        {
            UpdateHp();
            
            UpdateHpBarAmount();
            
            if (_hpProp.Value >= HpBarConst.MaxHp) OnCallback?.Invoke();
        }

        /// <summary>
        /// HPを更新
        /// </summary>
        private void UpdateHp()
        {
            _hpProp.Value = Mathf.Clamp(_hpProp.Value+HpBarConst.AdditionalHp, HpBarConst.MinHp, HpBarConst.MaxHp);
        }
        
        /// <summary>
        /// 充填値を更新
        /// </summary>
        private void UpdateHpBarAmount()
        {
            _hpBarFillAmountProp.Value = Mathf.Clamp(_hpProp.Value*HpBarConst.BarFillAmountMagnification, HpBarConst.BarFillAmountMin, HpBarConst.BarFillAmountMax);
        }
    }
}
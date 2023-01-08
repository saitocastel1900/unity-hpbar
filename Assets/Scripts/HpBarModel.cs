using System;
using Const;
using UniRx;
using UnityEngine;

namespace Gauge
{
    /// <summary>
    /// 
    /// </summary>
    public class HpBarModel
    {
        public IReadOnlyReactiveProperty<int> HpProp => _hpProp;
        private IntReactiveProperty _hpProp;

        public IReadOnlyReactiveProperty<float> HpBarAmountProp => _hpBarAmountProp;
        private FloatReactiveProperty _hpBarAmountProp;
        
        public event Action OnCallback;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HpBarModel(int hp=0,float fillAmount=0.0f)
        {
            _hpProp=new IntReactiveProperty(hp);
            _hpBarAmountProp=new FloatReactiveProperty(fillAmount);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateHp()
        {
            _hpProp.Value = Mathf.Clamp(_hpProp.Value+HpBarConst.AdditionalHp, HpBarConst.MinHp, HpBarConst.MaxHp);
            _hpBarAmountProp.Value = Mathf.Clamp(_hpProp.Value*0.1f, HpBarConst.BarAmountMin, HpBarConst.BarAmountMax);
            if (_hpProp.Value >= HpBarConst.MaxHp) OnCallback?.Invoke();
        }
    }
}
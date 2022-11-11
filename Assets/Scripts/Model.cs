using System;
using UniRx;
using UnityEngine;

namespace Gauge
{
    public class Model
    {
        public IReadOnlyReactiveProperty<int> Value => _value;
        private IntReactiveProperty _value;

        public event Action OnCallback;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="value"></param>
        public Model(int value=0)
        {
            _value=new IntReactiveProperty(value);
        }

        /// <summary>
        /// スコアを更新（加算）
        /// </summary>
        /// <param name="value"></param>
        public void UpdateCount(int value)
        {
            _value.Value = Mathf.Clamp(value, 0, 10);
            if (_value.Value >= 10) OnCallback?.Invoke();
        }
    }
}
using UniRx;
using UnityEngine;

namespace Gauge
{
    public class Model : MonoBehaviour
    {
        [SerializeField] private IntReactiveProperty _value = new IntReactiveProperty(0);
        public IReadOnlyReactiveProperty<int> Value => _value;

        public void UpdateCount(int value)
        {
            _value.Value = Mathf.Clamp(value, 0, 10);
        }
    }
}
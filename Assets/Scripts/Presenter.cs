using UniRx;
using UnityEngine;

namespace Gauge
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] private Model _model;
        [SerializeField] private View _view;

        private void Start()
        {
            //model=>view
            _model.Value
                .Subscribe(x =>
                    {
                        _view.UpdateText(x);
                        _view.GaugeValue = x;
                        _view.GaugeAnimation();
                        if (x >= 10) _view.UninteractiveClick();
                    },
                    ex => Debug.LogError("OnError!"),
                    () => Debug.Log("OnCompleted!")).AddTo(this);

            //view=>model
            _view.ObserbableClickButton()
                .Select(_ => +1)
                .Subscribe(
                    value => _model.UpdateCount(_model.Value.Value + (int) value),
                    ex => Debug.LogError("OnError!"),
                    () => Debug.Log("")).AddTo(this);
        }
    }
}
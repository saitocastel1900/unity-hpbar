using DG.Tweening;
using UnityEngine.UI;

namespace Utility
{
    /// <summary>
    /// 
    /// </summary>
    public static class HpBarAnimationUtility
    {
        /// <summary>
        /// 
        /// </summary>
        public static Tweener FillAmountTween(Image  hpBar,float gaugeValue,float duration=0.2f)
        {
            return hpBar.DOFillAmount(gaugeValue,duration).SetEase(Ease.OutCubic);
        }
    }
}
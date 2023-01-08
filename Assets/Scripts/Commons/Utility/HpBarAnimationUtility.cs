using DG.Tweening;
using UnityEngine.UI;

namespace Utility
{
    /// <summary>
    /// HPバーのアニメーションのUtilityクラス
    /// </summary>
    public static class HpBarAnimationUtility
    {
        /// <summary>
        /// HPバーアニメーションのTweener
        /// </summary>
        public static Tweener FillAmountTweener(Image  hpBar,float gaugeValue,float duration=0.2f)
        {
            return hpBar.DOFillAmount(gaugeValue,duration).SetEase(Ease.OutCubic);
        }
    }
}
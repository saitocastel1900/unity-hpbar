using DG.Tweening;

namespace Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class TweenerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static void KillIfNotNull(this Tweener tweener,bool complete=false)
        {
            if (tweener != null)
            {
                tweener.Kill(complete);
                tweener = null;
            }
        }
    }
}
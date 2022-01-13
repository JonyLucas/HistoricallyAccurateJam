using System.Linq;
using UnityEngine;

namespace Game.Extensions
{
    public static class AnimatorExtension
    {
        public static float GetAnimationClipDuration(this Animator animator, string clipName)
        {
            var animation = animator.runtimeAnimatorController
                .animationClips.FirstOrDefault(x => x.name == clipName);

            return animation != null ? animation.length : 0;
        }
    }
}
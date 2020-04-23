using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Animate Material Float Action")]
    public class AnimateMaterialFloatAction : AnimateMaterialPropertyAction<float>
    {
        protected override float EvaluateStartPoint(ref Material Context)
        {
            return Context.GetFloat(PropertyName);
        }

        protected override float Lerp(float A, float B, float t)
        {
            return Mathf.Lerp(A, B, t);
        }

        protected override void Set(ref Material Context, float Data)
        {
            Context.SetFloat(PropertyName, Data);
        }
    }
}

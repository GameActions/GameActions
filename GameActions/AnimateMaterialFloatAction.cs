using UnityEngine;

namespace GameActions
{
    public class AnimateMaterialFloatAction : AnimateMaterialPropertyAction<float>
    {
        protected override float EvaluateStartPoint(ref Material Context)
        {
            return Context.GetFloat(PropertyName);
        }

        protected override float Lerp(float Start, float Target, float LerpTime)
        {
            return Mathf.Lerp(Start, Target, LerpTime);
        }

        protected override void Set(ref Material Context, float Data)
        {
            Context.SetFloat(PropertyName, Data);
        }
    }
}

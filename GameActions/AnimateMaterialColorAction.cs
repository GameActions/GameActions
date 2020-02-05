using UnityEngine;

namespace GameActions
{
    public class AnimateMaterialColorAction : AnimateMaterialPropertyAction<Color>
    {
        protected override Color EvaluateStartPoint(ref Material Context)
        {
            return Context.GetColor(PropertyName);
        }

        protected override Color Lerp(Color Start, Color Target, float LerpTime)
        {
            return Color.Lerp(Start, Target, LerpTime);
        }

        protected override void Set(ref Material Context, Color Data)
        {
            Context.SetColor(PropertyName, Data);
        }
    }
}

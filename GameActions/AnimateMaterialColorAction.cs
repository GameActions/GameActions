using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Animate Material Color Action")]
    public class AnimateMaterialColorAction : AnimateMaterialPropertyAction<Color>
    {
        protected override Color EvaluateStartPoint(ref Material Context)
        {
            return Context.GetColor(PropertyName);
        }

        protected override Color Lerp(Color A, Color B, float t)
        {
            return Color.Lerp(A, B, t);
        }

        protected override void Set(ref Material Context, Color Data)
        {
            Context.SetColor(PropertyName, Data);
        }
    }
}

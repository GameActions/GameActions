using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Animate Position")]
    public class AnimatePositionAction : AnimateAction<Vector3, Transform>
    {
        protected override Transform InitializeContext(ActParameters Parameters, ref bool Success)
        {
            return Parameters.Object.transform;
        }

        protected override Vector3 EvaluateStartPoint(ref Transform Context)
        {
            return Context.localPosition;
        }

        protected override Vector3 Lerp(Vector3 A, Vector3 B, float t)
        {
            return Vector3.Lerp(A, B, t);
        }

        protected override void Set(ref Transform Context, Vector3 Data)
        {
            Context.localPosition = Data;
        }
    }
}

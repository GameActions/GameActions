using UnityEngine;

namespace GameActions
{
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

        protected override Vector3 Lerp(Vector3 Start, Vector3 Target, float LerpTime)
        {
            return Vector3.Lerp(Start, Target, LerpTime);
        }

        protected override void Set(ref Transform Context, Vector3 Data)
        {
            Context.localPosition = Data;
        }
    }
}

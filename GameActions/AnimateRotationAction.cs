using UnityEngine;

namespace GameActions
{
    public class AnimateRotationAction : AnimateActionWithDataConversion<Vector3, Quaternion, Transform>
    {
        [Space]
        public bool UseSlerp = true;

        protected override Transform InitializeContext(ActParameters Parameters, ref bool Success)
        {
            return Parameters.Object.transform;
        }

        protected override Quaternion EvaluateStartPoint(ref Transform Context)
        {
            return Context.localRotation;
        }

        protected override Quaternion ConvertData(Vector3 Data)
        {
            return Quaternion.Euler(Data);
        }

        protected override Quaternion Lerp(Quaternion Start, Quaternion Target, float LerpTime)
        {
            return UseSlerp
                    ? Quaternion.Slerp(Start, Target, LerpTime)
                    : Quaternion.Lerp(Start, Target, LerpTime);
        }

        protected override void Set(ref Transform Context, Quaternion Data)
        {
            Context.localRotation = Data;
        }
    }
}

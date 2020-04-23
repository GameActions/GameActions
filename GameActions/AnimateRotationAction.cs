using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Animate Rotation Action")]
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

        protected override Quaternion Lerp(Quaternion A, Quaternion B, float t)
        {
            return UseSlerp
                    ? Quaternion.Slerp(A, B, t)
                    : Quaternion.Lerp(A, B, t);
        }

        protected override void Set(ref Transform Context, Quaternion Data)
        {
            Context.localRotation = Data;
        }
    }
}

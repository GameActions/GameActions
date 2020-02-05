using System;
using System.Threading.Tasks;
using GameActions.Utilities;
using UnityEngine;

namespace GameActions
{
    public abstract class AnimateActionWithDataConversion<AnimationPropertyType, AnimationDataType, ContextType> : GameActionWithTargetObject
    {
        [Space]
        [Tooltip("Animation target value")]
        public AnimationPropertyType Target;
        public float Duration = 1;
        public Interpolators.InterpolationType InterpolationType = Interpolators.InterpolationType.ConstantAcceleration;

        protected abstract ContextType InitializeContext(ActParameters Parameters, ref bool Success);
        protected abstract AnimationDataType EvaluateStartPoint(ref ContextType Context);
        protected abstract AnimationDataType ConvertData(AnimationPropertyType Data);
        protected abstract AnimationDataType Lerp(
            AnimationDataType A,
            AnimationDataType B,
            float t
        );
        protected abstract void Set(
            ref ContextType Context,
            AnimationDataType Data
        );

        protected override async Task Act(ActParameters Parameters)
        {
            bool Success = true;
            var context = InitializeContext(Parameters, ref Success);
            if (!Success)
            {
                Debug.unityLogger.LogWarning("Animation initialization failed.", this);
                return;
            }

            var target = ConvertData(Target);
            if (Duration <= 0)
            {
                Set(ref context, target);
                return;
            }
            float Speed = 1 / Duration;

            var start = EvaluateStartPoint(ref context);
            for (float t = 0; t < 1; t += Time.deltaTime * Speed)
            {
                Set(ref context, Lerp(
                    start,
                    target,
                    Interpolators.Interpolate(InterpolationType, t)
                ));
                await Parameters.Yield();
            }
            Set(ref context, target);
        }
    }
}

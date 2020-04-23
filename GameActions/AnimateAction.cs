using System;
using System.Threading.Tasks;
using GameActions.Utilities;
using UnityEngine;

namespace GameActions
{
    public abstract class AnimateAction<AnimationDataType, ContextType> : GameActionWithTargetObject
    {
        [Space]
        [Tooltip("Animation target value")]
        public AnimationDataType Target;
        [Tooltip("Sets to the target immediately if Duration is equal to or less than 0")]
        public float Duration = 1;
        public Interpolators.InterpolationType InterpolationType = Interpolators.InterpolationType.ConstantAcceleration;

        protected abstract ContextType InitializeContext(ActParameters Parameters, ref bool Success);
        protected abstract AnimationDataType EvaluateStartPoint(ref ContextType Context);
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
                Debug.LogWarning("Animation initialization failed.", this);
                return;
            }

            if (Duration <= 0)
            {
                Set(ref context, Target);
                return;
            }
            float Speed = 1 / Duration;

            var start = EvaluateStartPoint(ref context);
            for (float t = 0; t < 1; t += Time.deltaTime * Speed)
            {
                Set(ref context, Lerp(
                    start,
                    Target,
                    Interpolators.Interpolate(InterpolationType, t)
                ));
                await Parameters.Yield();
            }
            Set(ref context, Target);
        }
    }
}

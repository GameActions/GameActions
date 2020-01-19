using System.Collections.Generic;

namespace GameActions.Utilities
{
    public static class Interpolators
    {
        public enum InterpolationType
        {
            Linear,
            ConstantAcceleration,
            ConstantAccelerationStart,
            ConstantAccelerationStop
        }

        public static float Interpolate(InterpolationType Type,float t)
        {
            switch (Type)
            {
                case InterpolationType.Linear: return Linear(t);
                case InterpolationType.ConstantAcceleration: return ConstantAcceleration(t);
                case InterpolationType.ConstantAccelerationStart: return ConstantAccelerationStart(t);
                case InterpolationType.ConstantAccelerationStop: return ConstantAccelerationStop(t);
                default: return Linear(t);
            }
        }

        public static float Linear(float t) { return t; }

        public static float ConstantAcceleration(float t)
        {
            if (t <= 0)
                return 0;
            else if (t <= 0.5f)
                return t * t * 2;
            else if (t < 1)
            {
                t = 1 - t;
                return 1 - t * t * 2;
            }
            else return 1;
        }

        public struct ConstantAccelerationLength
        {
            public readonly float AccelerationLength;
            public readonly float HalfAccelerationLength;
            public readonly float SpeedLimit;
            public readonly float Acceleration;
            public readonly float HalfAcceleration;
            public readonly float LinearPartOffset;
            public readonly float HalfLinearPartOffset;
            public ConstantAccelerationLength(float AccelerationLength)
            {
                if (Cache.TryGetValue(AccelerationLength, out this))
                    return;
                this.AccelerationLength = AccelerationLength;
                HalfAccelerationLength = AccelerationLength / 2;
                // AccelerationLength => l
                // SpeedLimit => h
                // l * (h / 2) + (1 - l) * h = 1  (because the speed integral should be 1 and the curve should be continues)
                // => h - (h * l / 2) = 1
                // => h = 1 / (1 - l/2)
                SpeedLimit = 1f / (1f - HalfAccelerationLength);
                Acceleration = AccelerationLength == 0 ? 1 : SpeedLimit / AccelerationLength;
                HalfAcceleration = Acceleration / 2;
                // (SpeedLimit / 2) * AccelerationLength - AccelerationLength * SpeedLimit
                // => SpeedLimit * (AccelerationLength / 2 - AccelerationLength)
                // => SpeedLimit * (-AccelerationLength / 2)
                // => -SpeedLimit * AccelerationLength / 2
                LinearPartOffset = -SpeedLimit * AccelerationLength / 2;
                // (SpeedLimit / 2) * HalfAccelerationLength - HalfAccelerationLength * SpeedLimit
                // => SpeedLimit * (HalfAccelerationLength / 2 - HalfAccelerationLength)
                // => SpeedLimit * (-HalfAccelerationLength / 2)
                // => -SpeedLimit * HalfAccelerationLength / 2
                // => -SpeedLimit * (AccelerationLength / 2) / 2
                // => LinearPartOffset / 2
                HalfLinearPartOffset = LinearPartOffset / 2;
                Cache[AccelerationLength] = this;
            }

            private static Dictionary<float, ConstantAccelerationLength> Cache = new Dictionary<float, ConstantAccelerationLength>();
        }
        public static float ConstantAcceleration(float t, ConstantAccelerationLength info)
        {
            if (t <= 0)
                return 0;
            else if (t < info.HalfAccelerationLength)
                return t * t * info.Acceleration;
            else if (t < 1 - info.HalfAccelerationLength)
                return t * info.SpeedLimit + info.HalfLinearPartOffset;
            else if (t < 1)
            {
                t = 1 - t;
                return 1 - t * t * info.Acceleration;
            }
            else return 1;
        }

        public static float ConstantAcceleration(float t, float ConstantAccelerationLength)
        {
            return ConstantAcceleration(t, new ConstantAccelerationLength(ConstantAccelerationLength));
        }

        public static float ConstantAccelerationStart(float t, ConstantAccelerationLength info)
        {
            if (t <= 0)
                return 0;
            else if (t < info.AccelerationLength)
                return t * t * info.HalfAcceleration;
            else if (t < 1)
                return t * info.SpeedLimit + info.LinearPartOffset;
            else return 1;
        }

        public static float ConstantAccelerationStart(float t, float ConstantAccelerationLength = 1)
        {
            return ConstantAccelerationStart(t, new ConstantAccelerationLength(ConstantAccelerationLength));
        }

        public static float ConstantAccelerationStop(float t, ConstantAccelerationLength info)
        {
            if (t <= 0)
                return 0;
            else if (t < 1 - info.AccelerationLength)
                return t * info.SpeedLimit;
            else if (t < 1)
            {
                t = 1 - t;
                return 1 - t * t * info.HalfAcceleration;
            }
            else return 1;
        }

        public static float ConstantAccelerationStop(float t, float ConstantAccelerationLength = 1)
        {
            return ConstantAccelerationStop(t, new ConstantAccelerationLength(ConstantAccelerationLength));
        }
    }
}

using System.Threading.Tasks;
using GameActions.Utilities;
using UnityEngine;

namespace GameActions
{
    public class MoveAction : GameAction
    {
        public Vector3 Destination;
        public float Speed = 1;
        public Interpolators.InterpolationType InterpolationType = Interpolators.InterpolationType.ConstantAcceleration;

        protected override async Task Act(ActParameters Parameters)
        {
            var start_pos = Parameters.Object.transform.localPosition;
            for (float t = 0; t < 1; t += Time.deltaTime * Speed)
            {
                Parameters.Object.transform.localPosition = Vector3.Lerp(
                    start_pos,
                    Destination,
                    Interpolators.Interpolate(InterpolationType, t)
                );
                await Parameters.Yield();
            }
            Parameters.Object.transform.localPosition = Destination;
        }
    }
}

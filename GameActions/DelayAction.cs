using GameActions.Utilities;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class DelayAction : GameAction
    {
        public float Duration;

        protected override async Task Act(GameObject Object, AnimationToken AnimationToken)
        {
            float initial_time = Time.time;
            while (Time.time - initial_time < Duration)
                await Task.Yield();
        }
    }
}

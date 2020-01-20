using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class DelayAction : GameAction
    {
        public float Duration;

        protected override async Task Act(GameObject Object, Func<Task> Yield)
        {
            float initial_time = Time.time;
            while (Time.time - initial_time < Duration)
                await Yield();
        }
    }
}

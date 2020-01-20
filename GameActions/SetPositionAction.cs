using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class SetPositionAction : GameAction
    {
        public Vector3 Destination;

        protected override Task Act(GameObject Object, Func<Task> Yield)
        {
            Object.transform.localPosition = Destination;
            return Task.CompletedTask;
        }
    }
}

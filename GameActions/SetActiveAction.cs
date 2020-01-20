using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class SetActiveAction : GameAction
    {
        public bool ActivationState = true;

        protected override Task Act(GameObject Object, Func<Task> Yield)
        {
            Object.SetActive(ActivationState);
            return Task.CompletedTask;
        }
    }
}

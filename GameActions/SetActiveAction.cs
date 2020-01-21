using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class SetActiveAction : GameAction
    {
        public bool ActivationState = true;

        protected override Task Act(ActParameters Parameters)
        {
            Parameters.Object.SetActive(ActivationState);
            return Task.CompletedTask;
        }
    }
}

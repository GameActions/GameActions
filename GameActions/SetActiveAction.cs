using GameActions.Utilities;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class SetActiveAction : GameAction
    {
        public bool ActivationState = true;

        protected override Task Act(GameObject Object, AnimationToken AnimationToken)
        {
            Object.SetActive(ActivationState);
            return Task.CompletedTask;
        }
    }
}

using GameActions.Utilities;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class ToggleActivationAction : GameAction
    {
        protected override Task Act(GameObject Object, AnimationToken AnimationToken)
        {
            Object.SetActive(!Object.activeSelf);
            return Task.CompletedTask;
        }
    }
}

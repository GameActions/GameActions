using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class ToggleActivationAction : GameAction
    {
        protected override Task Act(GameObject Object, Func<Task> Yield)
        {
            Object.SetActive(!Object.activeSelf);
            return Task.CompletedTask;
        }
    }
}

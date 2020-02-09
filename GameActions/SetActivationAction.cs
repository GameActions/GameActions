using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Set Activation")]
    public class SetActivationAction : GameActionWithTargetObject
    {
        public bool ActivationState = true;

        protected override Task Act(ActParameters Parameters)
        {
            Parameters.Object.SetActive(ActivationState);
            return Task.CompletedTask;
        }
    }
}

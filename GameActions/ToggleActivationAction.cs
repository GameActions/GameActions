using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Toggle Activation Action")]
    public class ToggleActivationAction : GameActionWithTargetObject
    {
        protected override Task Act(ActParameters Parameters)
        {
            Parameters.Object.SetActive(!Parameters.Object.activeSelf);
            return Task.CompletedTask;
        }
    }
}

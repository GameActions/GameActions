using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Stop GameAction")]
    public class StopAction : GameAction
    {
        [Tooltip("The action to stop")]
        public GameAction Action = null;

        protected override Task Act(ActParameters Parameters)
        {
            Action?.StopActing();
            return Task.CompletedTask;
        }
    }
}

using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class ForwardAction : GameAction
    {
        [Tooltip("The action to forward to")]
        public GameAction Action = null;

        protected override async Task Act(ActParameters Parameters)
        {
            // To prevent loops that make Unity unresponsive
            await Parameters.Yield();
            await Action?.Act();
        }
    }
}

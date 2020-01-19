using GameActions.Utilities;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class SetPositionAction : GameAction
    {
        public Vector3 Destination;

        protected override Task Act(GameObject Object, AnimationToken AnimationToken)
        {
            Object.transform.localPosition = Destination;
            return Task.CompletedTask;
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class GameActionSequence : GameAction
    {
        public GameAction[] Actions(GameObject Object)
        {
            //GetComponentsInChildren<GameAction>(); // only the 1-deep components are needed
            List<GameAction> result = new List<GameAction>();
            foreach (Transform child in Object.transform)
            {
                var components = child.GetComponents<GameAction>();
                foreach (var component in components)
                    result.Add(component);
            }
            return result.ToArray();
        }

        protected override async Task Act(ActParameters Parameters)
        {
            List<Task> Tasks = new List<Task>();
            foreach (var action in Actions(Parameters.Object))
                if (action.gameObject != gameObject)
                    await Parameters.Await(action.Act());
        }
    }
}

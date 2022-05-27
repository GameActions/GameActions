using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Action Containers/Random Action Set")]
    public class GameActionRandomSet : GameAction
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
            GameAction[] actions = Actions(Parameters.Object);
            if (actions.Length > 0)
                await actions[Random.Range(0, actions.Length)].Act();
        }
    }
}

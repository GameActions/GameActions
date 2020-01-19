using GameActions.Utilities;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public abstract class GameAction : MonoBehaviour
    {
        [SerializeField] private GameObject Object;
        public GameObject Object
        {
            get
            {
                if (Object == null)
                    Object = this.gameObject;
                return Object;
            }
            set
            {
                Object = value;
                _AnimationToken = null;
            }
        }
        private Dictionary<GameObject, AnimationToken> tokens = new Dictionary<GameObject, AnimationToken>();
        private AnimationToken _AnimationToken;
        private AnimationToken AnimationToken
        {
            get
            {
                if (_AnimationToken == null)
                {
                    if (!tokens.ContainsKey(Object))
                        tokens[Object] = new AnimationToken();
                    _AnimationToken = tokens[Object];
                }
                return _AnimationToken;
            }
        }
        protected abstract Task Act(GameObject Object, AnimationToken AnimationToken);
        public Task Act()
        {
            return Act(Object, AnimationToken);
        }
        public void ActReturningVoid() => Act();
    }
}

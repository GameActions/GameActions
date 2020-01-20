using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameActions.Utilities;
using UnityEngine;

namespace GameActions
{
    public abstract class GameAction : MonoBehaviour
    {
        [SerializeField] private GameObject _Object;
        public GameObject Object
        {
            get
            {
                if (_Object == null)
                    _Object = this.gameObject;
                return _Object;
            }
            set
            {
                _Object = value;
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

        protected abstract Task Act(GameObject Object, Func<Task> Yield);

        private class ActionTerminationException : System.Exception {}
        public async Task Act() // TODO: Rename?
        {
            var token = AnimationToken.GetToken();
            async Task Yield()
            {
                await Task.Yield();
                if (!token.IsValid)
                    throw new ActionTerminationException();
            }
            try
            {
                await Act(Object, Yield);
            }
            catch (ActionTerminationException) {}
        }

        public async void ActReturningVoid() // TODO: Rename to Start?
        {
            try
            {
                await Act();
            }
            catch (Exception e)
            {
                Debug.LogError(e, this);
            }
        }
    }
}

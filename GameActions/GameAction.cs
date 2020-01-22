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

        private static Dictionary<
            (GameObject, Type),
            AnimationToken
        > AnimationTokens = new Dictionary<(GameObject, Type), AnimationToken>();

        private AnimationToken _AnimationToken;
        private AnimationToken AnimationToken
        {
            get
            {
                if (_AnimationToken == null)
                {
                    var key = (Object, GetType());
                    if (!AnimationTokens.ContainsKey(key))
                        AnimationTokens[key] = new AnimationToken();
                    _AnimationToken = AnimationTokens[key];
                }
                return _AnimationToken;
            }
        }

        protected struct ActParameters
        {
            /// <summar>The object to act on</summar>
            public GameObject Object;
            /// <summar>Use this to yield</summar>
            public Func<Task> Yield;
            /// <summar>Use this to await anything the right way</summar>
            public Func<Task, Task> Await;
        }

        /// <summary>
        ///   Do not await anything without using Parameters.Await.
        ///   Use Parameters.Object if a target GameObject is required.
        /// </summary>
        protected abstract Task Act(ActParameters Parameters);

        private class ActionTerminationException : System.Exception {}
        public async Task Act() // TODO: Rename?
        {
            var token = AnimationToken.GetToken();

            async Task Await(Task task)
            {
                await task;
                if (!token.IsValid)
                    throw new ActionTerminationException();
            }
            async Task Yield()
            {
                await Task.Yield();
                if (!token.IsValid)
                    throw new ActionTerminationException();
            }

            try
            {
                await Act(new ActParameters {
                    Object = Object,
                    Yield = Yield,
                    Await = Await
                });
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

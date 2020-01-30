using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameActions.Utilities;
using UnityEngine;

namespace GameActions
{
    public abstract class GameAction : MonoBehaviour
    {
        protected virtual GameObject GetObject()
        {
            return gameObject;
        }

        // To store the Objects targeted by a GameAction to remove from the AnimationTokens later.
        private static Dictionary<Type, List<GameObject>> GameActionObjects
                 = new Dictionary<Type, List<GameObject>>();

        private static Dictionary<(Type, GameObject), AnimationToken> AnimationTokens
                 = new Dictionary<(Type, GameObject), AnimationToken>();

        private GameObject LastObject = null;
        private AnimationToken _AnimationToken = null;
        private AnimationToken AnimationToken
        {
            get
            {
                if (GetObject() != LastObject)
                {
                    (Type Type, GameObject Object) key = (GetType(), GetObject());
                    if (!AnimationTokens.ContainsKey(key))
                    {
                        if (!GameActionObjects.ContainsKey(key.Type))
                            GameActionObjects[key.Type] = new List<GameObject>();
                        GameActionObjects[key.Type].Add(key.Object);
                        AnimationTokens[key] = new AnimationToken();
                    }
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
        public async Task Act()
        {
            if (!isActiveAndEnabled)
                return;

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
                    Object = GetObject(),
                    Yield = Yield,
                    Await = Await
                });
            }
            catch (ActionTerminationException) {}
        }

        public async void StartActing()
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

        void OnDestroy()
        {
            var type = GetType();
            if (!GameActionObjects.ContainsKey(type))
                return;
            foreach (var obj in GameActionObjects[type])
                AnimationTokens.Remove((type, obj));
            GameActionObjects[type].Clear();
            GameActionObjects.Remove(type);
        }
    }
}

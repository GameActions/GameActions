using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameActions
{
    public class LoadSceneAction : GameAction
    {
        public string SceneName;

        protected override Task Act(ActParameters Parameters)
        {
            SceneManager.LoadScene(SceneName);
            return Task.CompletedTask;
        }
    }
}

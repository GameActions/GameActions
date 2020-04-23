using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Load Scene Action")]
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

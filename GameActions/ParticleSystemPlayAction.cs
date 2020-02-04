using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameActions
{
    public class ParticleSystemPlayAction : GameActionWithTargetObject
    {
        protected override Task Act(ActParameters Parameters)
        {
            var particle_system = Parameters.Object.GetComponent<ParticleSystem>();
            if (particle_system != null)
                particle_system.Play();
            else
                Debug.unityLogger.LogWarning("No ParticleSystem found.", this);
            return Task.CompletedTask;
        }
    }
}
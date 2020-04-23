using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Play Particle System Action")]
    public class PlayParticleSystemAction : GameActionWithTargetObject
    {
        protected override Task Act(ActParameters Parameters)
        {
            var particle_system = Parameters.Object.GetComponent<ParticleSystem>();
            if (particle_system != null)
                particle_system.Play();
            else
                Debug.LogWarning("No ParticleSystem found.", this);
            return Task.CompletedTask;
        }
    }
}

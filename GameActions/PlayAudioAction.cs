using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    [AddComponentMenu("GameActions/Actions/Play Audio Action")]
    public class PlayAudioAction : GameActionWithTargetObject
    {
        [Tooltip(
                "The AudioClips to play using the AudioSource on the target Object. "
                + "Will just play the AudioSource if empty."
            )]
        public AudioClip[] AudioClips = {};
        public bool PlayRandomAudioClip = true;

        private int CurrentIndex = 0;

        protected override Task Act(ActParameters Parameters)
        {
            AudioSource AudioSource = Parameters.Object.GetComponent<AudioSource>();
            if (AudioSource != null)
            {
                if (AudioClips != null && AudioClips.Length > 0)
                {
                    if (PlayRandomAudioClip)
                        CurrentIndex = Random.Range(0, AudioClips.Length);
                    else
                        CurrentIndex = (CurrentIndex + 1) % AudioClips.Length;
                    AudioSource.PlayOneShot(AudioClips[CurrentIndex]);
                }
                else
                    AudioSource.Play();
            }
            else
                Debug.LogWarning("No AudioSource.", this);

            return Task.CompletedTask;
        }
    }
}

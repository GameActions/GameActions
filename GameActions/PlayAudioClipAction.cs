using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameActions
{
    public class PlayAudioClipAction : GameAction
    {
        public AudioClip AudioClip = null;

        protected override Task Act(GameObject Object, Func<Task> Yield)
        {
            AudioSource AudioSource = Object.GetComponent<AudioSource>();
            if (AudioSource != null)
            {
                if (AudioClip != null)
                    AudioSource.PlayOneShot(AudioClip);
                else
                    AudioSource.Play();
            }
            else
                Debug.LogWarning("No AudioSource.", this);

            return Task.CompletedTask;
        }
    }
}

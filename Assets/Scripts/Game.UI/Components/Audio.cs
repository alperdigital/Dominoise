using UnityEngine;
using Game.Services;

namespace Game.UI
{
    public sealed class Audio : MonoBehaviour
    {
        [Header("Audio References")]
        [SerializeField] private AudioSource shutterAudioSource;
        [SerializeField] private AudioClip shutterSound;
        [SerializeField] private AudioClip countdownSound;
        [SerializeField] private AudioClip winSound;
        [SerializeField] private AudioClip loseSound;

        public void Initialize()
        {
            if (shutterAudioSource == null)
                shutterAudioSource = GetComponent<AudioSource>();

            if (shutterAudioSource != null)
            {
                shutterAudioSource.playOnAwake = false;
                shutterAudioSource.loop = false;
            }
        }

        public void PlayShutterSound()
        {
            PlaySound(shutterSound);
        }

        public void PlayCountdownSound()
        {
            PlaySound(countdownSound);
        }

        public void PlayWinSound()
        {
            PlaySound(winSound);
        }

        public void PlayLoseSound()
        {
            PlaySound(loseSound);
        }

        void PlaySound(AudioClip clip)
        {
            if (shutterAudioSource != null && clip != null)
            {
                shutterAudioSource.clip = clip;
                shutterAudioSource.Play();
            }
        }

        public void SetVolume(float volume)
        {
            if (shutterAudioSource != null)
                shutterAudioSource.volume = Mathf.Clamp01(volume);
        }
    }
}

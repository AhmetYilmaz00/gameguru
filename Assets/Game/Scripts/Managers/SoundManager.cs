using UnityEngine;

namespace Game.Scripts.Managers
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private float pitchAmount;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }


        public void PitchAndPlayAudio()
        {
            _audioSource.Play();
            _audioSource.pitch += pitchAmount;
        }
    }
}
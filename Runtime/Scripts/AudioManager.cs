using System.Collections;
using UnityEngine;

namespace RaiTools.Runtime
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        [SerializeField] private GameObject audioClipPrefab;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        public void PlaySound(AudioClip audioClip)
        {
            StartCoroutine(PlaySoundAsync(audioClip));
        }

        IEnumerator PlaySoundAsync(AudioClip audioClip)
        {
            GameObject tempGO = Instantiate(audioClipPrefab);
            AudioSource tempAS = tempGO.GetComponent<AudioSource>();

            tempAS.clip = audioClip;
            tempAS.gameObject.name = audioClip.name;
            tempAS.Play();
            
            while (tempAS.isPlaying)
            {
                yield return null;
            }

            Destroy(tempGO);
        }
    }
}
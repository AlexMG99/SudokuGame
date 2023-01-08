using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio.AudioSFX
{
    public class AudioSFX : MonoBehaviourSingleton<AudioSFX>
    {
        [Header("AudioSFX")]
        [Space]

        [TextArea(5, 5)]
        public string about;

        [Space]
        [Header("Parameters")]
        [SerializeField] private bool mute = false;
        [SerializeField] private AudioSource audioSourceReference;
        [SerializeField] private AudioSFX_SoundRegister SFXRegister;

        [Space]
        [Header("Pool settings")]
        public int poolSize = 20;
        private List<AudioSource> pool = new List<AudioSource>();

        // Start is called before the first frame update
        void Start()
        {
            GeneratePool();
            DontDestroyOnLoad(this.gameObject);
        }

        public void PlaySFX(string SFX_ID, float intensityFactor = 1)
        {
            if (!mute)
            {
                AudioSFX_SFX audioSFX = null;
                AudioSource usedSource = null;
                bool hasFoundSFX = false;

                //Trying to find the index in register based on SFX_ID

                for (int i = 0; i < SFXRegister.registeredAudioSFX.Length; i++)
                {
                    if (SFXRegister.registeredAudioSFX[i].ID == SFX_ID)
                    {
                        audioSFX = SFXRegister.registeredAudioSFX[i];
                        hasFoundSFX = true;
                        break;
                    }
                }

                if (!hasFoundSFX)
                {
                    Debug.LogWarning("AudioSFX : The SFX ID [" + SFX_ID + "] can't be found in the sound register");
                    return;
                }
                else
                {
                    usedSource = CheckIfASExist(SFX_ID);

                    if (!usedSource)
                    {
                        usedSource = GetUsableSource();
                        usedSource.name = "AudioSFX_" + SFX_ID;
                    }

                    // Play AudioSource
                    if (usedSource)
                    {
                        usedSource.pitch = Random.Range(audioSFX.minRandomPitch, audioSFX.maxRandomPitch);
                        usedSource.PlayOneShot(audioSFX.clip, intensityFactor * audioSFX.intensityFactor);
                    }
                    else
                    {
                        Debug.LogWarning("AudioSFX : Trying to play a null source!");
                    }
                }
            }
        }

        private void GeneratePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                AudioSource SFXSource = Instantiate(audioSourceReference, this.transform);
                pool.Add(SFXSource);
            }
        }

        private AudioSource GetUsableSource()
        {
            AudioSource source = null;

            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].isPlaying)
                {
                    source = pool[i];

                    return source;
                }
            }

            return source;
        }

        private AudioSource CheckIfASExist(string SFX_ID)
        {
            AudioSource source = null;

            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].name.Contains(SFX_ID))
                {
                    source = pool[i].GetComponent<AudioSource>();
                    return source;
                }
            }

            return source;
        }
    }
}

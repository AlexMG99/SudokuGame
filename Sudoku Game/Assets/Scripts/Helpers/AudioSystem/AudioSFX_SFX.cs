using System;
using UnityEngine;

namespace Audio.AudioSFX
{
    [Serializable]
    public class AudioSFX_SFX
    {
        public string ID = "SFX ID";

        public AudioClip clip;
        public float minRandomPitch = 0.9f;
        public float maxRandomPitch = 1.1f;
        [Range(0.00f, 1f)]
        public float intensityFactor = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Audio.AudioSFX
{
    [CreateAssetMenu(fileName = "SoundRegister", menuName = "ScriptableObjects/Audio/New Sound Register", order = 1)]
    public class AudioSFX_SoundRegister : ScriptableObject
    {
        public AudioSFX_SFX[] registeredAudioSFX;
    }
}

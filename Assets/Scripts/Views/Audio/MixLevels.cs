using UnityEngine;
using UnityEngine.Audio;

namespace Core.Audio
{
    public class MixLevels : MonoBehaviour
    {
        private const string GLOBAL_LEVEL = "GlobalLevel";
        private const string UNITS_LEVEL = "UnitsLevel";
        private const string MUSIC_LEVEL = "MusicLevel";
        private const string ENVIRONMENT_LEVEL = "EnvironmentLevel";

        private readonly float MinLevel = -20;
        private readonly float MaxLevel = 0;

        [SerializeField]
        private AudioMixer _masterMixer;

        private float _globalVolume;

        public float GlobalLevel
        {
            set
            {
                _masterMixer.SetFloat(GLOBAL_LEVEL, ConvertToMixLevel(value));
            }
            get
            {
                _masterMixer.GetFloat(GLOBAL_LEVEL, out float level);
                return Clamp01(level);
            }
        }
        public float MusicLevel
        {
            set
            {
                _masterMixer.SetFloat(MUSIC_LEVEL, ConvertToMixLevel(value));
            }
            get
            {
                _masterMixer.GetFloat(MUSIC_LEVEL, out float level);
                return Clamp01(level);
            }
        }
        public float EnvironmentLevel
        {
            set
            {
                _masterMixer.SetFloat(ENVIRONMENT_LEVEL, ConvertToMixLevel(value));
            }
            get
            {
                _masterMixer.GetFloat(ENVIRONMENT_LEVEL, out float level);
                return Clamp01(level);
            }
        }
        public float UnitsLevel
        {
            set
            {
                _masterMixer.SetFloat(UNITS_LEVEL, ConvertToMixLevel(value));
            }
            get
            {
                _masterMixer.GetFloat(UNITS_LEVEL, out float level);
                return Clamp01(level);
            }
        }

        private float ConvertToMixLevel(float volume) => Mathf.Lerp(MinLevel, MaxLevel, volume);
        private float Clamp01(float level)
        {
            if (level <= MinLevel) return 0;
            if (level >= MaxLevel) return 1;
            return 1 - Mathf.Abs(level / (MaxLevel - MinLevel));
        }

        public void Mute(bool isMuted)
        {
            GlobalLevel = isMuted ? 0 : _globalVolume;
        }
    }
}
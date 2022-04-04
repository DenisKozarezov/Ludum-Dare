using System.Collections;
using UnityEngine;

namespace Core.Audio
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _musicSource;
        [SerializeField]
        private AnimationCurve _fadeCurve;

        private IEnumerator FadeSound(float time = 3f)
        {
            if (_musicSource != null)
            {
                float factor = 0;
                while (factor < 1)
                {
                    _musicSource.volume = _fadeCurve.Evaluate(factor += Time.deltaTime / time);
                    yield return null;
                }
            }
        }
        public void Fade(float time = 3f)
        {
            StartCoroutine(FadeSound(time));
        }
    }
}
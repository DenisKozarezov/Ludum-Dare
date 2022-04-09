using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class VignetteComponent : MonoBehaviour
    {
        [SerializeField]
        private RawImage _image;

        public IEnumerator Fade(FadeMode mode, float time)
        {
            Color startColor = _image.color;
            startColor.a = mode == FadeMode.In ? 0 : 1;
            Color endColor = startColor;
            endColor.a = mode == FadeMode.In ? 1 : 0;

            float factor = 0;
            while (factor < 1)
            {
                _image.color = Color.Lerp(startColor, endColor, factor += Time.deltaTime / time);
                Debug.Log(factor);
                yield return null;
            }
            if (mode == FadeMode.Out) Destroy(gameObject);
        }
    }
}
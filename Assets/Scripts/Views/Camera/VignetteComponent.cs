using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class VignetteComponent : MonoBehaviour
    {
        [SerializeField]
        private RawImage _image;
        [SerializeField, Min(0)]
        private float _startTime;
        private TaskCompletionSource<bool> _taskCompletionSource;

        public async Task Fade(FadeMode mode, float time)
        {
            _taskCompletionSource = new TaskCompletionSource<bool>();
            StartCoroutine(FadeCoroutine(mode, time));
            await _taskCompletionSource.Task;
        }
        private IEnumerator FadeCoroutine(FadeMode mode, float time)
        {
            Color startColor = _image.color;
            startColor.a = mode == FadeMode.In ? 0 : 1;
            Color endColor = startColor;
            endColor.a = mode == FadeMode.In ? 1 : 0;

            float factor = 0;
            while (factor < 1)
            {
                _image.color = Color.Lerp(startColor, endColor, factor += Time.deltaTime / time);
                yield return null;
            }
            if (mode == FadeMode.Out) Destroy(gameObject);
            _taskCompletionSource.TrySetResult(true);
        }
    }
}
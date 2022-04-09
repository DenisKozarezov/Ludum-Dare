using System.Collections;
using UnityEngine;

namespace Core
{
    public static class CameraExtensions
    {
        private const string VignettePath = "Prefabs/UI/Vignette";
        public static IEnumerator Fade(FadeMode mode, float time = 3f)
        {
            var vignette = Resources.Load<VignetteComponent>(VignettePath);
            vignette.gameObject.SetActive(true);
            yield return vignette.StartCoroutine(GameObject.Instantiate(vignette).Fade(mode, time));
        }
    }
}
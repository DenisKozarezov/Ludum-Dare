using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public static class CameraExtensions
    {
        private const string VignettePath = "Prefabs/UI/Vignette";
        public static async Task Fade(FadeMode mode, float time = 3f)
        {
            var vignette = Resources.Load<VignetteComponent>(VignettePath);
            await GameObject.Instantiate(vignette).Fade(mode, time);
        }
    }
}
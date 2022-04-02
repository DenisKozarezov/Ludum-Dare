using UnityEngine;

namespace Core.Services
{
    public class GameManager : MonoBehaviour
    {
        private async void Start()
        {
            await CameraExtensions.Fade(FadeMode.Out);
        }
    }
}
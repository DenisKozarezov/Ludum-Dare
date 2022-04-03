using UnityEngine;

namespace Core.Services
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawn;

        private async void Start()
        {
            StartGame();
            await CameraExtensions.Fade(FadeMode.Out);
        }

        private void StartGame()
        {
            var player = UnitsManager.InstantiateUnit(0);
            player.transform.position = _playerSpawn.transform.position;
        }
    }
}
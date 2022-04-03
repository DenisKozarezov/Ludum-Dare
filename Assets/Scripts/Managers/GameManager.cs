using System;
using UnityEngine;

namespace Core.Services
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawn;

        private event Action GameOver;

        private async void Start()
        {
            StartGame();
            await CameraExtensions.Fade(FadeMode.Out);
        }

        private void StartGame()
        {
            var player = UnitsManager.InstantiateUnit(0);
            player.Died += GameOverFunction;
            player.transform.position = _playerSpawn.transform.position;
        }

        private void GameOverFunction()
        {
#if UNITY_EDITOR
            Debug.Log("<b><color=green>[GAME]</color></b>: Game over.");
#endif
            GameOver?.Invoke();
        }
    }
}
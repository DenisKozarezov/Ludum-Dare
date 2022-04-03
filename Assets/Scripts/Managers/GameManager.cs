using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Core.UI;

namespace Core.Services
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Transform _playerSpawn;
        [SerializeField]
        private MenuController _gameMenu;

        [Header("Options")]
        [SerializeField, Min(0)]
        private float _preparationTime;

        public UnityEvent GameStart;
        public UnityEvent GameOver;

        private async void Start()
        {
            StartGame();
            await CameraExtensions.Fade(FadeMode.Out);
        }

        private async void StartGame()
        {
            var player = UnitsManager.InstantiateUnit(0);
            player.Died += GameOverFunction;
            player.transform.position = _playerSpawn.transform.position;

            await Task.Delay(TimeSpan.FromSeconds(_preparationTime));
            GameStart?.Invoke();
        }

        private void GameOverFunction()
        {
#if UNITY_EDITOR
            Debug.Log("<b><color=green>[GAME]</color></b>: Game <b><color=yellow>over</color></b>.");
#endif

            _gameMenu.Enable = false;

            GameOver?.Invoke();
        }
    }
}
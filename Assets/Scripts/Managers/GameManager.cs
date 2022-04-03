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

        private void ExitGame()
        {
            if (Application.isEditor)
            {
#if UNITY_EDITOR
                EditorExtensions.Log("Player <b><color=yellow>quited</color></b> game.");
                UnityEditor.EditorApplication.isPlaying = false;
#endif
            }
            else
            {
                Application.Quit();
            }
        }
        public async void ExitGame_UnityEditor()
        {
            var form = UI.Forms.DecisionForm.CreateForm();
            form.SetDescription("Are you sure?");
            bool isConfirmed = await form.AwaitForDecision();
            if (isConfirmed)
            {
                ExitGame();
            }
        }
        private void RestartGame()
        {
#if UNITY_EDITOR
            Debug.Log("<b><color=green>[GAME]</color></b>: Player begun <b><color=yellow>new game</color></b>.");
#endif


        }
        public async void RestartGame_UnityEditor()
        {
            var form = UI.Forms.DecisionForm.CreateForm();
            form.SetDescription("You want to restart?");
            bool isConfirmed = await form.AwaitForDecision();
            if (isConfirmed)
            {
                RestartGame();
            }
        }
    }
}
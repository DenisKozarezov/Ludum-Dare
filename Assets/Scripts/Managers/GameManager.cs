using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Core.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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

        private IEnumerator Start()
        {
            StartCoroutine(StartGame());
            yield return CameraExtensions.Fade(FadeMode.Out);
        }

        private IEnumerator StartGame()
        {
            var player = UnitsManager.InstantiateUnit(0);
            player.Died += GameOverFunction;
            player.transform.position = _playerSpawn.transform.position;

            yield return new WaitForSeconds(_preparationTime);

#if UNITY_EDITOR
            Debug.Log("<b><color=green>[GAME]</color></b>: Game <b><color=yellow>started</color></b>.");
#endif

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
            form.SetLabel("Exit");
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
        private IEnumerator BackToMainMenu()
        {
#if UNITY_EDITOR
            Debug.Log("<b><color=green>[GAME]</color></b>: Player <b><color=yellow>has left</color></b> the game.");
#endif

            yield return CameraExtensions.Fade(FadeMode.In, 2f);
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        public async void BackToMainMenu_UnityEditor()
        {
            var form = UI.Forms.DecisionForm.CreateForm();
            form.SetDescription("You want to leave?");
            bool isConfirmed = await form.AwaitForDecision();
            if (isConfirmed)
            {
                StartCoroutine(BackToMainMenu());
            }
        }
    }
}
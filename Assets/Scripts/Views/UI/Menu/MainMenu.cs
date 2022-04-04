using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.UI
{
    public class MainMenu : MenuState
    {
        private async void Start()
        {
            await CameraExtensions.Fade(FadeMode.Out);
        }

        public async void StartGame_UnityEditor()
        {
            await CameraExtensions.Fade(FadeMode.In);
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
        public void Settings_UnityEditor()
        {
            MenuController.SwitchState(MenuStates.Settings);
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
            var form = Forms.DecisionForm.CreateForm();
            form.SetLabel("Exit");
            form.SetDescription("Are you sure?");
            bool isConfirmed = await form.AwaitForDecision();
            if (isConfirmed)
            {
                ExitGame();
            }
        }
    }
}
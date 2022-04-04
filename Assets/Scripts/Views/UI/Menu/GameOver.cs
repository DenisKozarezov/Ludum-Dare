using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core.UI
{
    public class GameOver : MenuState
    {
        public UnityEvent StartNew;

        public async void BackToMainMenu_UnityEditor()
        {
            await CameraExtensions.Fade(FadeMode.In);
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
        }
        public void OpenSettings_UnityEditor()
        {
            MenuController?.SwitchState(MenuStates.Settings);
        }
    }
}
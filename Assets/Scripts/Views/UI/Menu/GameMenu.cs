using System;
using UnityEngine;

namespace Core.UI
{
    public class GameMenu : MenuState
    {
        public event Action Settings;
        public event Action Quit;

        private void QuitGame()
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

        public void OpenSettings_UnityEditor()
        {
            MenuController?.SwitchState(MenuStates.Settings);
            Settings?.Invoke();
        }
        public async void ExitGame_UnityEditor()
        {
            var form = Forms.DecisionForm.CreateForm();
            form.SetDescription("Are you sure?");
            bool isConfirmed = await form.AwaitForDecision();
            if (isConfirmed)
            {
                Quit?.Invoke();
                QuitGame();
            }
        }
    }
}
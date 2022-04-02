using System;

namespace Core.UI
{
    public class GameMenu : MenuState
    {
        public event Action Settings;
        public event Action ExitGame;

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
                ExitGame?.Invoke();
            }
        }
    }
}
using System;

namespace Core.UI
{
    public class GameMenu : MenuState
    {
        public event Action Settings;

        public void OpenSettings_UnityEditor()
        {
            MenuController?.SwitchState(MenuStates.Settings);
            Settings?.Invoke();
        }
    }
}
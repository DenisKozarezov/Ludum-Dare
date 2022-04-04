using UnityEngine.Events;

namespace Core.UI
{
    public class GameOver : MenuState
    {
        public UnityEvent StartNew;
        public UnityEvent BackToMainMenu;

        public void BackToMainMenu_UnityEditor()
        {
            BackToMainMenu?.Invoke();
        }
        public void OpenSettings_UnityEditor()
        {
            MenuController?.SwitchState(MenuStates.Settings);
        }
    }
}
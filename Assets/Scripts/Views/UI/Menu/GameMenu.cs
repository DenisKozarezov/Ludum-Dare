using UnityEngine.Events;

namespace Core.UI
{
    public class GameMenu : MenuState
    {
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
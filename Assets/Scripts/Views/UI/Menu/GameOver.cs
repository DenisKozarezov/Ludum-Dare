using UnityEngine.Events;

namespace Core.UI
{
    public class GameOver : MenuState
    {
        public UnityEvent StartNew;

        public void OpenSettings_UnityEditor()
        {
            MenuController?.SwitchState(MenuStates.Settings);
        }
    }
}
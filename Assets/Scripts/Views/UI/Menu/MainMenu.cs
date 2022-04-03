using UnityEngine;

namespace Core.UI
{
    public class MainMenu : MenuState
    {
        public void StartGame_UnityEditor()
        {

        }
        public void Settings_UnityEditor()
        {
            MenuController.SwitchState(MenuStates.Settings);
        }
    }
}
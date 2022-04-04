using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    public class GameOver : MenuState
    {
        [SerializeField]
        private AudioSource _audioSource;

        public UnityEvent StartNew;
        public UnityEvent BackToMainMenu;

        private void OnEnable()
        {
            _audioSource.Play();
        }

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
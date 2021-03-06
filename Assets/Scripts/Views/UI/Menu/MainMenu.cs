using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.UI
{
    public class MainMenu : MenuState
    {
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private AudioClip _newGameSound;

        public UnityEvent StartNew;

        private async void Start()
        {
            await CameraExtensions.Fade(FadeMode.Out);
        }

        private void SetInteractable(bool isInteractable)
        {
            foreach (var button in GetComponentsInChildren<Button>())
            {
                button.interactable = isInteractable;
            }
        }

        public async void StartGame_UnityEditor()
        {
            StartNew?.Invoke();
            SetInteractable(false);
            _audioSource.PlayOneShot(_newGameSound);
            
            await CameraExtensions.Fade(FadeMode.In);
            await Task.Delay(System.TimeSpan.FromSeconds(2f));
            
            SceneManager.LoadScene(1, LoadSceneMode.Single);
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
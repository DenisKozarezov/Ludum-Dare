using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    public class SettingsView : MenuState
    {
        [SerializeField]
        private RectTransform _contentTransform;

        public UnityEvent SettingsReset;
        public UnityEvent SettingsApplied;

        private void Awake()
        {
            SettingsReset.AddListener(OnSettingsReset);
            SettingsApplied.AddListener(OnSettingsApplied);
        }

        private void OnSettingsReset()
        {
#if UNITY_EDITOR
            Debug.Log("<b><color=green>[SETTINGS]</color></b>: All settings were <b><color=yellow>successfully</color></b> reset.");
#endif
        }
        private void OnSettingsApplied()
        {
#if UNITY_EDITOR
            Debug.Log("<b><color=green>[SETTINGS]</color></b>: All settings were <b><color=yellow>successfully</color></b> applied and saved.");
#endif
        }
   
        public async void Reset_UnityEditor()
        {
            var form = Forms.DecisionForm.CreateForm();
            form.SetLabel("Reset");
            form.SetDescription("Are you sure?");
            bool isConfirmed = await form.AwaitForDecision();

            if (isConfirmed)
            {
                SettingsReset?.Invoke();
            }
        }
        public async void Apply_UnityEditor()
        {
            var form = Forms.DecisionForm.CreateForm();
            form.SetLabel("Apply settings");
            form.SetDescription("Are you sure?");
            bool isConfirmed = await form.AwaitForDecision();

            if (isConfirmed)
            {
                SettingsApplied?.Invoke();
            }
        }
    }
}
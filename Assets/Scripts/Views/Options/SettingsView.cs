using UnityEngine;
using UnityEngine.Events;
using Core.Audio;
using Core.UI.Visual;

namespace Core.UI
{
    public class SettingsView : MenuState
    {
        [SerializeField]
        private RectTransform _contentTransform;
        [SerializeField]
        private MixLevels _mixLevels;
        
        [Header("Settings Elements")]
        [SerializeField]
        private SliderVisualElement _globalVolume;
        [SerializeField]
        private SliderVisualElement _unitsVolume;
        [SerializeField]
        private SliderVisualElement _musicVolume;
        [SerializeField]
        private SliderVisualElement _environmentVolume;

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
            _mixLevels.GlobalLevel = _globalVolume.Value;
            _mixLevels.UnitsLevel = _unitsVolume.Value;
            _mixLevels.MusicLevel = _musicVolume.Value;
            _mixLevels.EnvironmentLevel = _environmentVolume.Value;

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
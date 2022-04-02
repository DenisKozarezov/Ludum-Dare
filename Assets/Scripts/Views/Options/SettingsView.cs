using UnityEngine;
using UnityEngine.Events;

namespace Core.UI
{
    public class SettingsView : MenuState
    {
        [SerializeField]
        private RectTransform _contentTransform;
        private int _currentTab = 0;

        public UnityEvent SettingsApplied;

        private void Awake()
        {
            SettingsApplied.AddListener(OnSettingsApplied);
        }

        private void OnSettingsApplied()
        {
#if UNITY_EDITOR
            Debug.Log("<b><color=green>[SETTINGS]</color></b>: All settings were <b><color=yellow>successfully</color></b> applied and saved.");
#endif
        }
        public void OpenTab(int index)
        {
            if (_contentTransform.childCount == 0 || _currentTab == index || index < 0) return;

            _currentTab = index;
            for (int i = 0; i < _contentTransform.childCount; i++)
            {
                _contentTransform.GetChild(i).gameObject.SetActive(false);
            }

            if (index >= _contentTransform.childCount) return;
            var tab = _contentTransform.GetChild(index);
            tab.gameObject.SetActive(true);
        }
        public async void Apply_UnityEditor()
        {
            var form = Forms.DecisionForm.CreateForm();
            form.SetLabel("Options");
            form.SetDescription("Are you sure?");
            bool isConfirmed = await form.AwaitForDecision();

            if (isConfirmed)
            {
                SettingsApplied?.Invoke();
            }
        }
    }
}
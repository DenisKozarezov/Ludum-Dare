using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Core.UI.Forms
{
    [RequireComponent(typeof(RectTransform))]
    internal class DecisionForm : MonoBehaviour, IDecisionAwaiter, IClosableForm, IAutoSizable
    {
        private const string FormPath = "Prefabs/UI/Forms/Decision Form";

        [Header("References")]
        [SerializeField]
        private RectTransform _rectTransform;
        [SerializeField]
        private TMP_Text _label;
        [SerializeField]
        private TMP_Text _text;
        [SerializeField]
        private Button _okButton;
        [SerializeField]
        private Button _cancelButton;
        [SerializeField]
        private Button _closeButton;

        [Header("Options")]
        [SerializeField]
        private bool _autoSize;
        [SerializeField, Min(0)]
        private float _minHeight;

        private TaskCompletionSource<bool> _taskCompletion;

        public bool AutoSize => _autoSize;
        public float MinHeight => _minHeight;

        private void Awake()
        {
            _okButton.onClick.AddListener(OnAccept);
            _cancelButton.onClick.AddListener(OnCancelled);
            if (_closeButton != null) _closeButton.onClick.AddListener(OnCancelled);
        }
        private void OnAccept()
        {
            _taskCompletion.SetResult(true);
        }
        private void OnCancelled()
        {
            _taskCompletion.SetResult(false);
        }

        public static IDecisionAwaiter CreateForm()
        {
            var obj = Instantiate(Resources.Load<GameObject>(FormPath));
            return obj.GetComponentInChildren<IDecisionAwaiter>();
        }
        public async Task<bool> AwaitForDecision()
        {
            if (AutoSize)
            {
                _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, MinHeight + _text.preferredHeight);
            }

            gameObject.SetActive(true);
            _taskCompletion = new TaskCompletionSource<bool>();
            var result = await _taskCompletion.Task;
            Close();
            return result;
        }
        public void SetLabel(string label)
        {
            _label.text = label;
        }
        public void SetDescription(string text)
        {
            _text.text = text;
        }
        public void Close()
        {
            Destroy(gameObject);
        }
    }
}
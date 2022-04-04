using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Core.UI.Visual
{
    internal class SelectorVisualElement : MonoBehaviour
    {       
        private int _currentIndex;

        [Header("Options")]
        [SerializeField]
        private bool _interactable = true;
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private Button _previousButton;
        [SerializeField]
        private Button _nextButton;
        [Space, SerializeField]
        private string[] _items;

        public UnityEvent<string> OnValueChanged { get; set; } = new UnityEvent<string>();
        public UnityEvent<int> OnSelectedIndexChanged { get; set; } = new UnityEvent<int>();

        public int SelectedIndex
        {
            get => _currentIndex;
            set
            {
                if (value < 0 || value >= _items.Length) throw new System.ArgumentOutOfRangeException();
                _currentIndex = value;
            }
        }
        public string SelectedItem => _items[SelectedIndex];
        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                if (_previousButton != null) _previousButton.interactable = value;
                if (_nextButton != null) _nextButton.interactable = value;
            }
        }

        private void Awake()
        {
            UpdateIndex(0);
            _previousButton?.onClick.AddListener(OnPreviousButtonClicked);
            _nextButton?.onClick.AddListener(OnNextButtonClicked);
        }
        private void OnValidate()
        {
            Interactable = _interactable;
        }

        private void OnPreviousButtonClicked() => UpdateIndex(_currentIndex - 1);
        private void OnNextButtonClicked() => UpdateIndex(_currentIndex + 1);
        private void CheckInteraction(int index)
        {
            if (index <= 0)
            {
                _previousButton.interactable = false;
                return;
            }

            if (index >= _items.Length - 1)
            {
                _nextButton.interactable = false;
                return;
            }

            Interactable = true;
        }
        private void UpdateIndex(int index)
        {
            if (index < 0 || index >= _items.Length) return;

            _currentIndex = index;
            if (_text != null) _text.text = SelectedItem;

            CheckInteraction(index);
            
            OnValueChanged.Invoke(SelectedItem);
            OnSelectedIndexChanged.Invoke(SelectedIndex);
        }       
    }
}
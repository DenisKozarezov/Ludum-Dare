using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Core.UI.Visual
{
    internal class SliderVisualElement : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField]
        private bool _interactable = true;
        [SerializeField]
        private byte _precision = 1;
        [Space, SerializeField]
        private Slider _slider;
        [SerializeField]
        private TextMeshProUGUI _valueText;
        [SerializeField]
        private float _minValue = 0f;
        [SerializeField]
        private float _maxValue = 1f;

        public float Value
        {
            get => _slider.value;
            set
            {
                _slider.value = Mathf.Clamp(value, _minValue, _maxValue);
                _slider.value = Precise(value, _precision);
                OnSliderUpdate(_slider.value);
            }
        }
        public bool Interactable
        {
            get => _interactable;
            set
            {
                _interactable = value;
                _slider.interactable = value;
            }
        }
        public Slider.SliderEvent OnValueChanged => _slider.onValueChanged;

        private void Awake()
        {
            OnValueChanged.AddListener(OnSliderUpdate);
        }
        private void OnValidate()
        {
            if (_slider != null)
            {
                Interactable = _interactable;
                _slider.minValue = _minValue;
                _slider.maxValue = _maxValue;
            }
        }
        private void OnSliderUpdate(float value)
        {
            _valueText.text = Precise(value, _precision).ToString();
        }
        private float Precise(float value, byte precision) => (float)System.Math.Round(value, _precision);
    }
}
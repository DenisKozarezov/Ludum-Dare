using UnityEngine;
using Core.Abilities;

namespace Core.UI
{
    public class AbilitiesPanel : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _abilitiesTransform;
        [SerializeField]
        private GameObject _abilitySlotPrefab;

        private void Awake()
        {
            Clear();
        }

        public void AddAbility(Ability ability)
        {
            GameObject slot = Instantiate(_abilitySlotPrefab, _abilitiesTransform);
            AbilityView view = slot.GetComponent<AbilityView>();

            view.SetData(ref ability);
            view.name = $"Ability {ability.DisplayName}";
            view.transform.SetAsLastSibling();
        }
        public void RemoveAbility(int index)
        {
            if (index < _abilitiesTransform.childCount)
            {
                Destroy(_abilitiesTransform.GetChild(index).gameObject);
            }
        }
        public void Clear()
        {
            for (int i = 0; i < _abilitiesTransform.childCount; i++)
            {
                RemoveAbility(i);
            }
        }
    }
}
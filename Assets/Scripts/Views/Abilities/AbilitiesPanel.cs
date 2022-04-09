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

        private void OnAbilityCast(Ability ability)
        {
            if (ability.Effect != null)
            {
#if UNITY_EDITOR
                Debug.Log($"<b><color=green>[ABILITIES]</color></b>: Player is executing <b><color=yellow>{ability.DisplayName}</color></b> effect.");
#endif
                ability.Effect.Execute(Units.PlayerView.Instance);
            }
        }

        public void AddAbility(Ability ability)
        {
            GameObject slot = Instantiate(_abilitySlotPrefab, _abilitiesTransform);
            AbilityView view = slot.GetComponent<AbilityView>();

            view.SetData(ref ability);
            view.Cast += OnAbilityCast;
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
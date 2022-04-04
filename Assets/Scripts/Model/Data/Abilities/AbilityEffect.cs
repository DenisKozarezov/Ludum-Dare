using UnityEngine;

namespace Core.Abilities
{
    public abstract class AbilityEffect : ScriptableObject
    {
        [Header("Options")]
        [SerializeField]
        private uint _id;
        [SerializeField]
        private string _displayName;
        [SerializeField, TextArea]
        private string _description;
        [SerializeField]
        private string _effectPrefabPath;

        protected GameObject CreateEffect()
        {
            if (string.IsNullOrEmpty(_effectPrefabPath)) return null;

            Vector2 position = Units.PlayerView.Instance.transform.position;
            var prefab = Instantiate(Resources.Load<GameObject>(_effectPrefabPath), position, Quaternion.identity);
            return prefab;
        }

        public abstract void Execute();
    }
}
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

        public abstract void Execute();
    }
}
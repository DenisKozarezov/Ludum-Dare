using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Abilities
{
    public abstract class AbilityEffect : ScriptableObject
    {
        [SerializeField]
        private uint _id;
        [SerializeField]
        private string _displayName;
        [SerializeField]
        private string _description;

        public abstract void Execute();
    }
}
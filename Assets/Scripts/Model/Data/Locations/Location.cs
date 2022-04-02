using UnityEngine;
using Core.Units;

namespace Core
{
    public class Location : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField]
        private PlayerSpawner _spawner;

        public void Lockdown()
        {

        }
        public void EnableSpawning(bool isSpawning)
        {
            _spawner.Enable(isSpawning);
        }
    }
}
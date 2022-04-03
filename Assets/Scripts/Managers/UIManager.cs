using UnityEngine;
using Core.UI;

namespace Core.Services
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private MenuController _gameOverController;

        private void Awake()
        {
            _gameOverController.Enable = false;
        }
        public void ShowGameOver()
        {
            _gameOverController.SwitchState(MenuStates.Menu);
        }
    }
}
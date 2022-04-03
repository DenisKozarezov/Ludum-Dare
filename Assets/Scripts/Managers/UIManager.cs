using UnityEngine;
using Core.UI;

namespace Core.Services
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField]
        private MenuController _gameOverController;

        public void ShowGameOver()
        {
            _gameOverController.SwitchState(MenuStates.Menu);
        }
    }
}
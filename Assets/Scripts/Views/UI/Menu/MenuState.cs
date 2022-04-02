using UnityEngine;

namespace Core.UI
{
    public abstract class MenuState : MonoBehaviour
    {
        protected MenuController MenuController;

        public void Init(MenuController menuController)
        {
            MenuController = menuController;
        }
        public void GetBack()
        {
            MenuController.GetBack();
        }
    }
}
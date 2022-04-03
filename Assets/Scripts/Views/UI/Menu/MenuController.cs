using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

namespace Core.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private bool _autoActiveWhenStart;
        [SerializeField]
        private bool _closeWhenLast;
        [SerializeField]
        private SerializableDictionaryBase<MenuStates, MenuState> _states;

        private MenuStates CurrentState { get; set; }
        private readonly Stack<MenuStates> StateHistory = new Stack<MenuStates>();

        private void Start()
        {
            foreach (var state in _states)
            {
                if (state.Value == null) continue;

                state.Value.Init(this);
            }

            foreach (var state in _states)
            {
                state.Value.gameObject.SetActive(false);
            }
            _states.First().Value.gameObject.SetActive(_autoActiveWhenStart);
        }
        private void Update()
        {
            if (UnityEngine.Input.GetKeyUp(KeyCode.Escape))
            {
                GetBack();
            }
        }

        public void SwitchState(MenuStates state, bool getBack = false)
        {
            if (!_states.ContainsKey(state) || _states[state] == null) return;

            if (_states.TryGetValue(CurrentState, out MenuState currentState))
            {
                currentState.gameObject.SetActive(false);
            }
            CurrentState = state;
            _states[state].gameObject.SetActive(true);
            _states[state].transform.SetAsLastSibling();

            if (!getBack)
            {
                StateHistory.Push(state);
            }

#if UNITY_EDITOR
            Debug.Log($"<b><color=green>[MENU CONTROLLER]</color></b>: Switching to state <b><color=yellow>{state}</color></b>.");
#endif
        }
        public void GetBack()
        {
            switch (StateHistory.Count)
            {
                case 0:
                    SwitchState(MenuStates.Menu);
                    break;
                case 1:
                    if (_closeWhenLast)
                    {
                        StateHistory.Pop();
                        _states[CurrentState].gameObject.SetActive(false);
                    }
                    break;
                default:
                    StateHistory.Pop();
                    SwitchState(StateHistory.Peek(), true);
                    break;
            }
        }
    }
}
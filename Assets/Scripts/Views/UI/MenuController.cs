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
        private SerializableDictionaryBase<MenuStates, MenuState> _states;

        private MenuStates _startState;
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
            var startState = _states.First();
            _startState = startState.Key;
            SwitchState(_startState);
            startState.Value.gameObject.SetActive(_autoActiveWhenStart);
        }

        public void SwitchState(MenuStates state, bool getBack = false)
        {
            if (!_states.ContainsKey(state) || _states[state] == null) return;

            if (CurrentState != MenuStates.Menu && _states.TryGetValue(CurrentState, out MenuState currentState))
            {
                currentState.gameObject.SetActive(false);
            }
            CurrentState = state;
            _states[state].gameObject.SetActive(true);

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
            if (CurrentState == MenuStates.Menu) return;

            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
            {
                _states[CurrentState].GetBack();
            }
        }
        public void CloseLastWindow()
        {
            if (StateHistory.Count <= 1)
            {
                SwitchState(_startState);
            }
            else
            {
                StateHistory.Pop();
                SwitchState(StateHistory.Peek(), true);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Units.State
{
    public interface IStateMachine<T>
    {
        BaseState<T> CurrentState { get; }
        void SwitchState<State>() where State : BaseState<T>;
    }

    public abstract class BaseStateMachine<T>
    {
        protected List<BaseState<T>> States { set; get; } = new List<BaseState<T>>();
        public BaseState<T> CurrentState { private set; get; }

        public void SwitchState<State>() where State : BaseState<T>
        {
            CurrentState?.Exit();
            CurrentState = States.FirstOrDefault(state => state is State);

#if UNITY_EDITOR
            if (CurrentState == null)
            {
                EditorExtensions.Log($"{CurrentState.Unit} Unable to switch <b><color=yellow>'{typeof(State).Name}'</color></b> state: there is no such state in list.", EditorExtensions.LogType.Assert);
            }
#endif

            CurrentState?.Enter();
        }
    }
}
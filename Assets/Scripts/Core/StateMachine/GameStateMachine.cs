using System;
using System.Collections.Generic;
using Core.StateMachine.States;
using UnityEngine;

namespace Core.StateMachine
{
    public class GameStateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states;
        private IGameState _currentState;
        
        public GameStateMachine(InitializeState initializeState, GameLoopState gameLoopState, EndGameState endGameState)
        {
            _states = new Dictionary<Type, IGameState>
            {
                { typeof(InitializeState), initializeState },
                { typeof(GameLoopState), gameLoopState },
                { typeof(EndGameState), endGameState }
            };
            _currentState = _states[typeof(InitializeState)];
        }

        public void ChangeState<T>() where T : IGameState
        {
            var type = typeof(T);

            if (!_states.TryGetValue(type, out var newState))
            {
                Debug.LogError($"State {type.Name} is not registered in the GameStateMachine.");
                return;
            }

            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter(ChangeStateRequest);
        }

        private void ChangeStateRequest(Type stateType)
        {
            _currentState?.Exit();
            _currentState = _states[stateType];
            _currentState.Enter(ChangeStateRequest);
        }

        public void Tick()
        {
            _currentState.Tick();
        }
    }
}

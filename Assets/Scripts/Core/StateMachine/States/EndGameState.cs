using System;
using Services;
using Views;

namespace Core.StateMachine.States
{
    public class EndGameState : IGameState
    {
        private readonly InputService _inputService;
        private readonly GameEndView _endView;
        private Action<Type> _requestSwitch;

        public EndGameState(InputService inputService, GameEndView endView)
        {
            _inputService = inputService;
            _endView = endView;
        }
        
        public void Enter(Action<Type> requestSwitch)
        {
            _endView.ShowGameEndPanel();

            UnsubscribeEvents();
            _endView.RestartButtonClicked += HandleRestart;
            _inputService.RestartRequested += HandleRestart;
            
            _requestSwitch = requestSwitch;
        }

        public void Exit()
        {
            _endView.HideGameEndPanel();
            UnsubscribeEvents();
        }

        public void Tick() { }
        
        private void UnsubscribeEvents()
        {
            _endView.RestartButtonClicked -= HandleRestart;
            _inputService.RestartRequested -= HandleRestart;
        }

        private void HandleRestart()
        {
            _requestSwitch(typeof(InitializeState));
        }
    }
}

using System;
using Controllers;
using Services;

namespace Core.StateMachine.States
{
    public class GameLoopState : IGameState
    {
        private readonly BoardController _boardController;
        private readonly InputService _inputService;
        private readonly BoardViewController _boardViewController;
        private Action<Type> _requestSwitch;
        
        public GameLoopState(BoardController boardController, InputService inputService, BoardViewController boardViewController)
        {
            _boardController = boardController;
            _inputService = inputService;
            _boardViewController = boardViewController;
        }
        
        public void Enter(Action<Type> requestSwitch)
        {
            UnsubscribeEvents();
            
            _boardController.OnGameOver += HandleGameEnd;
            _boardController.OnVictory += HandleGameEnd;
            _inputService.RestartRequested += HandleRestart;

            _requestSwitch = requestSwitch;
        }

        public void Exit()
        {
            UnsubscribeEvents();
            _boardViewController.Dispose();
        }

        public void Tick()
        {
            _inputService.ProcessRestartInput();
        }
        
        private void UnsubscribeEvents()
        {
            _boardController.OnGameOver -= HandleGameEnd;
            _boardController.OnVictory -= HandleGameEnd;
            _inputService.RestartRequested -= HandleRestart;
        }
        
        private void HandleGameEnd()
        {
            _requestSwitch(typeof(EndGameState));
        }
        
        private void HandleRestart()
        {
            _requestSwitch(typeof(InitializeState));
        }
    }
}

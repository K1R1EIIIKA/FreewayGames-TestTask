using System;
using Configs;
using Controllers;
using Services.Factories;

namespace Core.StateMachine.States
{
    public class InitializeState : IGameState
    {
        private readonly IBoardFactory _boardFactory;
        private readonly BoardController _boardController;
        private readonly BoardViewController _boardViewController;
        private readonly GameConfig _gameConfig;
        private Action<Type> _requestSwitch;

        public InitializeState(IBoardFactory boardFactory, BoardController boardController, BoardViewController boardViewController, GameConfig gameConfig)
        {
            _boardFactory = boardFactory;
            _boardController = boardController;
            _boardViewController = boardViewController;
            _gameConfig = gameConfig;
        }
        
        public void Enter(Action<Type> requestSwitch)
        {
            var board = _boardFactory.Create();
            _boardController.SetBoard(_boardFactory, board, _gameConfig);
            _boardViewController.Initialize(board);
            
            _requestSwitch = requestSwitch;
            _requestSwitch(typeof(GameLoopState));
        }

        public void Exit() { }

        public void Tick() { }
    }
}

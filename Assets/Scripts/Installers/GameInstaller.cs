using Zenject;
using Configs;
using Controllers;
using Core.StateMachine;
using Core.StateMachine.States;
using Services;
using Services.Factories;
using Services.Strategies;
using UnityEngine;
using Views; 

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _config;
        [SerializeField] private BoardView _boardView;
        [SerializeField] private GameEndView _endView;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_config).AsSingle();
            Container.BindInstance(_boardView).AsSingle();
            Container.BindInstance(_endView).AsSingle();

            Container.Bind<InputService>().AsSingle();

            Container.Bind<ICellStrategy>().WithId(CellActionType.Open).To<OpenCellStrategy>().AsSingle();
            Container.Bind<ICellStrategy>().WithId(CellActionType.Flag).To<FlagCellStrategy>().AsSingle();

            Container.Bind<ICellFactory>().To<CellFactory>().AsSingle();
            Container.Bind<IBoardFactory>().To<BoardFactory>().AsSingle();

            Container.Bind<BoardController>().AsSingle();
            Container.Bind<BoardViewController>().AsSingle();

            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
            Container.Bind<InitializeState>().AsSingle();
            Container.Bind<GameLoopState>().AsSingle();
            Container.Bind<EndGameState>().AsSingle();
        }
    }
}

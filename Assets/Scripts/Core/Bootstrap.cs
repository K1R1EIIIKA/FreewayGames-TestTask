using Core.StateMachine;
using Core.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Bootstrap : MonoBehaviour
    {
        private IStateMachine _gameStateMachine;
        
        [Inject]
        private void Construct(IStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }
        
        private void Start()
        {
            _gameStateMachine.ChangeState<InitializeState>();
        }
        
        private void Update()
        {
            _gameStateMachine.Tick();
        }
    }
}

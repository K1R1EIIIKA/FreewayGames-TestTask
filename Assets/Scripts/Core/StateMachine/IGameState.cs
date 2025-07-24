using System;

namespace Core.StateMachine
{
    public interface IGameState
    {
        void Enter(Action<Type> requestStateSwitch);
        void Exit();
        void Tick();
    }
}

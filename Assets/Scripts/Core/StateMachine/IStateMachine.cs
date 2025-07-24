namespace Core.StateMachine
{
	public interface IStateMachine
	{
		void ChangeState<T>() where T : IGameState;
		void Tick();
	}
}
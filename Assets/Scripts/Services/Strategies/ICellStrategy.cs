using Controllers;

namespace Services.Strategies{
	public interface ICellStrategy
	{
		void Execute(BoardController controller, int x, int y);
	}
}
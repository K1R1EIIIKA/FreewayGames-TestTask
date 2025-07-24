using Controllers;

namespace Services.Strategies
{
	public class FlagCellStrategy: ICellStrategy
	{
		public void Execute(BoardController controller, int x, int y)
		{
			controller.FlagCell(x, y);
		}
	}
}
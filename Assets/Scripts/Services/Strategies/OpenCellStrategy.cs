using Controllers;

namespace Services.Strategies
{
	public class OpenCellStrategy: ICellStrategy
	{
		public void Execute(BoardController controller, int x, int y)
		{
			controller.OpenCell(x, y);
		}
	}
}
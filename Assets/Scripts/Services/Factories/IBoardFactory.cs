using Models;

namespace Services.Factories
{
	public interface IBoardFactory
	{
		BoardModel Create();
		void PlaceMines(BoardModel board, int mines, int ignoreX, int ignoreY);
		void CalculateNeighborMines(BoardModel board);
	}
}
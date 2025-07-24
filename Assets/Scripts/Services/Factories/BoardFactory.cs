using Configs;
using Models;

namespace Services.Factories
{
	public class BoardFactory: IBoardFactory
	{
		private readonly GameConfig _config;
		private readonly ICellFactory _cellFactory;
		
		public BoardFactory(GameConfig config, ICellFactory cellFactory)
		{
			_config = config;
			_cellFactory = cellFactory;
		}
		
		public BoardModel Create()
		{
			var rows = _config.Rows;
			var columns = _config.Columns;
			
			var board = new BoardModel(rows, columns);

			for (var x = 0; x < rows; x++)
			for (var y = 0; y < columns; y++)
			{
				board.Cells[x, y] = _cellFactory.Create(x, y, false);
			}

			return board;
		}
		
		public void PlaceMines(BoardModel board, int mines, int ignoreX, int ignoreY)
		{
			var random = new System.Random();
			var placedMines = 0;

			while (placedMines < mines)
			{
				var x = random.Next(board.Rows);
				var y = random.Next(board.Columns);

				if (!board.Cells[x, y].HasMine && (x != ignoreX || y != ignoreY))
				{
					board.Cells[x, y].HasMine = true;
					placedMines++;
				}
			}
		}
		
		public void CalculateNeighborMines(BoardModel board)
		{
			for (var x = 0; x < board.Rows; x++)
			{
				for (var y = 0; y < board.Columns; y++)
				{
					if (board.Cells[x, y].HasMine) continue;

					var neighborMines = 0;

					for (var dx = -1; dx <= 1; dx++)
					{
						for (var dy = -1; dy <= 1; dy++)
						{
							if (dx == 0 && dy == 0) continue;

							var nx = x + dx;
							var ny = y + dy;

							if (nx >= 0 && nx < board.Rows && ny >= 0 && ny < board.Columns &&
							    board.Cells[nx, ny].HasMine)
							{
								neighborMines++;
							}
						}
					}

					board.Cells[x, y].NeighborMines = neighborMines;
				}
			}
		}
	}
}
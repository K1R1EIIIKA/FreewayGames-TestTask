namespace Models
{
	public class BoardModel
	{
		public readonly int Rows;
		public readonly int Columns;
		public readonly CellModel[,] Cells;
		
		public BoardModel(int rows, int columns)
		{
			Rows = rows;
			Columns = columns;
			Cells = new CellModel[rows, columns];
		}
	}
}
namespace Models
{
	public class CellModel
	{
		public readonly int X;
		public readonly int Y;
		public bool IsOpened;
		public bool IsFlagged;
		public bool HasMine;
		public int NeighborMines;
		
		public CellModel(int x, int y, bool hasMine)
		{
			X = x;
			Y = y;
			HasMine = hasMine;
			IsOpened = false;
			IsFlagged = false;
			NeighborMines = 0;
		}
	}
}
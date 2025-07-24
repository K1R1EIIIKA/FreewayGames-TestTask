using Models;

namespace Services.Factories
{
	public class CellFactory: ICellFactory
	{
		public CellModel Create(int x, int y, bool hasMine)
		{
			var cell = new CellModel(x, y, hasMine);
			return cell;
		}
	}
}
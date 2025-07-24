using Models;

namespace Services.Factories
{
	public interface ICellFactory
	{
		CellModel Create(int x, int y, bool hasMine);
	}
}
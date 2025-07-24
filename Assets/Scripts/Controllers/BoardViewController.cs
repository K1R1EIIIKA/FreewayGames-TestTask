using Models;
using Services;
using Services.Strategies;
using UnityEngine.EventSystems;
using Views;
using Zenject;

namespace Controllers
{
	public class BoardViewController
	{
		private readonly BoardController _boardController;
		private readonly InputService _inputService;
		private readonly ICellStrategy _openCellStrategy;
		private readonly ICellStrategy _flagCellStrategy;
		private readonly BoardView _boardView;
		
		public BoardViewController(BoardController boardController, InputService inputService, 
			[Inject(Id = CellActionType.Open)] ICellStrategy openCellStrategy, [Inject(Id = CellActionType.Flag)] ICellStrategy flagCellStrategy,
			BoardView boardView)
		{
			_boardController = boardController;
			_inputService = inputService;
			_openCellStrategy = openCellStrategy;
			_flagCellStrategy = flagCellStrategy;
			_boardView = boardView;
		}

		public void Initialize(BoardModel board)
		{
			_boardController.OnCellChanged -= OnCellChanged;
			_inputService.CellClicked -= OnCellClicked;
			_boardController.OnCellChanged += OnCellChanged;
			_inputService.CellClicked += OnCellClicked;

			_boardView.BuildBoard(board.Rows, board.Columns, board.Cells, _inputService);
		}
		
		public void Dispose()
		{
			_boardController.OnCellChanged -= OnCellChanged;
			_inputService.CellClicked -= OnCellClicked;
		}
		
		private void OnCellClicked(int x, int y, PointerEventData.InputButton button)
		{
			switch (button)
			{
				case PointerEventData.InputButton.Left:
					_boardController.ExecuteCellStrategy(x, y, _openCellStrategy);
					break;
				case PointerEventData.InputButton.Right:
					_boardController.ExecuteCellStrategy(x, y, _flagCellStrategy);
					break;
			}
		}

		private void OnCellChanged(CellModel cell)
		{
			if (!_boardView.CellViews.TryGetValue((cell.X, cell.Y), out var view))
			{
				return;
			}

			if (cell.IsOpened)
			{
				if (cell.HasMine)
				{
					view.SetExplodedMine();
				}
				else if (cell.NeighborMines > 0)
				{
					view.SetDigit(cell.NeighborMines);
				}
				else
				{
					view.SetOpened();
				}
			}
			else if (cell.IsFlagged)
			{
				view.SetFlag();
			}
			else
			{
				view.SetDefaultView();
			}
		}
	}
}
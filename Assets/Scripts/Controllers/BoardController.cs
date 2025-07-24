using System;
using Configs;
using Models;
using Services.Factories;
using Services.Strategies;
using UnityEngine;

namespace Controllers
{
	public class BoardController
	{
		public event Action OnGameOver;
		public event Action OnVictory;
		public event Action<CellModel> OnCellChanged;
		
		private BoardModel _board;
		private IBoardFactory _boardFactory;
		private GameConfig _gameConfig;
		private bool _isGameEnded;
		private bool _minesPlaced;
		
		public void SetBoard(IBoardFactory boardFactory, BoardModel board, GameConfig gameConfig)
		{
			_board = board;
			_boardFactory = boardFactory;
			_gameConfig = gameConfig;
			_isGameEnded = false;
			_minesPlaced = false;
		}
		
		public void ExecuteCellStrategy(int x, int y, ICellStrategy strategy)
		{
			if (_isGameEnded)
			{
				return;
			}

			if (!IsValid(x, y))
			{
				Debug.LogError($"Invalid cell coordinates: ({x}, {y})");
				return;
			}
			
			if (!_minesPlaced)
			{
				_boardFactory.PlaceMines(_board, _gameConfig.Mines, x, y);
				_boardFactory.CalculateNeighborMines(_board);
				_minesPlaced = true;
			}

			strategy.Execute(this, x, y);

			if (CheckGameOver(x, y))
			{
				_isGameEnded = true;
				RevealAllCells();
				OnGameOver?.Invoke();
			}
			else if (CheckVictory())
			{
				_isGameEnded = true;
				RevealAllCells();
				OnVictory?.Invoke();
			}
		}
		
		public void OpenCell(int x, int y)
		{
			var cell = _board.Cells[x, y];
			if (cell.IsOpened || cell.IsFlagged)
			{
				return;
			}

			cell.IsOpened = true;
			OnCellChanged?.Invoke(cell);

			if (cell.HasMine)
			{
				return;
			}

			if (cell.NeighborMines != 0)
			{
				return;
			}

			for (var dx = -1; dx <= 1; dx++)
			{
				for (var dy = -1; dy <= 1; dy++)
				{
					int nx = x + dx, ny = y + dy;
					if ((dx != 0 || dy != 0) && IsValid(nx, ny))
					{
						OpenCell(nx, ny);
					}
				}
			}
		}

		public void FlagCell(int x, int y)
		{
			var cell = _board.Cells[x, y];
			if (cell.IsOpened)
			{
				return;
			}

			cell.IsFlagged = !cell.IsFlagged;
			OnCellChanged?.Invoke(cell);
		}
		
		private void RevealAllCells()
		{
			foreach (var cell in _board.Cells)
			{
				if (cell.HasMine && !cell.IsOpened)
				{
					cell.IsFlagged = true;
					OnCellChanged?.Invoke(cell);
				}
				else if (!cell.IsOpened)
				{
					cell.IsOpened = true;
					OnCellChanged?.Invoke(cell);
				}
			}
		}

		private bool CheckGameOver(int x, int y)
		{
			return _board.Cells[x, y].HasMine && _board.Cells[x, y].IsOpened;
		}

		private bool CheckVictory()
		{
			foreach (var cell in _board.Cells)
			{
				if (!cell.HasMine && !cell.IsOpened)
				{
					return false;
				}
			}
			
			return true;
		}

		private bool IsValid(int x, int y)
		{
			return x >= 0 && y >= 0 && x < _board.Rows && y < _board.Columns;
		}
	}
}
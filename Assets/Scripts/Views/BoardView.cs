using System.Collections.Generic;
using Models;
using Services;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private CellView _cellPrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        
        private readonly Dictionary<(int, int), CellView> _cellViews = new();
        public IReadOnlyDictionary<(int, int), CellView> CellViews => _cellViews;
        
        public void BuildBoard(int rows, int columns, CellModel[,] Cells, InputService inputService)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            _cellViews.Clear();
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = columns;
            
            for (var x = 0; x < rows; x++)
            {
                for (var y = 0; y < columns; y++)
                {
                    var cellView = Instantiate(_cellPrefab, transform);
                    cellView.transform.localPosition = new Vector3(x, y, 0);
                    cellView.SetDefaultView();
                    cellView.Setup(Cells[x, y], inputService);
                    _cellViews[(x, y)] = cellView;
                }
            }
        }
    }
}

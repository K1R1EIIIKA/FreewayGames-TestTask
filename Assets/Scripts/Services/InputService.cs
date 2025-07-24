using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Services
{
    public class InputService
    {
        public event Action<int, int, PointerEventData.InputButton> CellClicked;
    
        public event Action RestartRequested;
        
        public void OnCellClicked(int x, int y, PointerEventData.InputButton button)
        {
            CellClicked?.Invoke(x, y, button);
        }
        
        public void ProcessRestartInput()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartRequested?.Invoke();
            }
        }
    }
}

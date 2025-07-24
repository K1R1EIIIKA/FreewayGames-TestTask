using System;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class GameEndView : MonoBehaviour
    {
        [SerializeField] private GameObject _gameEndPanel;
        [SerializeField] private Button _restartButton;
         
        public event Action RestartButtonClicked;
        
        private void Awake()
        {
            _restartButton.onClick.AddListener(() => RestartButtonClicked?.Invoke());
            _gameEndPanel.SetActive(false);
        }
        
        public void ShowGameEndPanel()
        {
            _gameEndPanel.SetActive(true);
        }

        public void HideGameEndPanel()
        {
            _gameEndPanel.SetActive(false);
        }
    }
}

using Models;
using Services;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views
{
    public class CellView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _background;
        [SerializeField] private GameObject _flag;
        [SerializeField] private GameObject _mine;
        [SerializeField] private GameObject _digit;
        [SerializeField] private TextMeshProUGUI _numberText;
        
        private int _x, _y;
        private InputService _inputService; 

        public void Setup(CellModel model, InputService inputService)
        {
            _x = model.X;
            _y = model.Y;
            _inputService = inputService;
        }
        
        public void SetDefaultView()
        {
            _background.color = Color.gray;
            _flag.SetActive(false);
            _mine.SetActive(false);
            _numberText.text = string.Empty;
        }
        
        public void SetOpened()
        {
            _background.color = Color.white;
            _flag.SetActive(false);
            _mine.SetActive(false);
            _digit.SetActive(false);
            _numberText.text = string.Empty;
        }
        
        public void SetDigit(int number)
        {
            _background.color = Color.white;
            _flag.SetActive(false);
            _mine.SetActive(false);
            _digit.SetActive(true);
            _numberText.text = number.ToString();
        }

        public void SetFlag()
        {
            _flag.SetActive(true);
            _mine.SetActive(false);
            _digit.SetActive(false);
            _background.color = Color.gray; 
            _numberText.text = string.Empty;
        }
        
        public void SetExplodedMine()
        {
            _background.color = Color.red;
            _mine.SetActive(true);
            _flag.SetActive(false);
            _digit.SetActive(false);
            _numberText.text = string.Empty;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _inputService.OnCellClicked(_x, _y, eventData.button);
        }
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AR.Birds
{
    public class ItemMenuBird : MonoBehaviour
    {
        [SerializeField] private Image _imageIcon;
        [SerializeField] private Image _imageFrame;
        [SerializeField] private TextMeshProUGUI _textPrice;
        [SerializeField] private GameObject _lock;

        private BirdInfo _birdInfo;
        private bool _isOpen = false;
        private UnityAction _actionSelected;
        public void Initialize(BirdInfo birdInfo, UnityAction actionSelected)
        {
            _birdInfo = birdInfo;
            _imageIcon.sprite = _birdInfo.sprite;
            _textPrice.text = _birdInfo.scoreToOpen.ToString();
            _isOpen = PlayerPrefs.GetInt(DataGameBird.KEY_QUANTITY_SCORE, 0) < _birdInfo.scoreToOpen ? false : true;
            _actionSelected = actionSelected;
            gameObject.GetComponent<Button>().onClick.AddListener(OnClickButtonBird);

            if (_birdInfo.typeBird == TypeBirds.Cardinal)
                ActivateBird();

            if(_isOpen)
            {
                _textPrice.gameObject.SetActive(false);
                _lock.SetActive(false);
            }
        }

        private void OnClickButtonBird()
        {
            if (!_isOpen)
                return;

            _actionSelected?.Invoke();
            ActivateBird();
            DataGameBird.NameCurentBird = _birdInfo.typeBird.ToString();
        }

        private void ActivateBird()
        {
            _imageFrame.color = Color.green;
        }
        public void DeactivateBird()
        {
            _imageFrame.color = Color.white;
        }

        private void OnDestroy()
        {
            gameObject.GetComponent<Button>().onClick.RemoveListener(OnClickButtonBird);
        }
    }
}

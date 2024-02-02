using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace AR.Birds
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private DataBirds _dataBirds;
        [SerializeField] private ItemMenuBird _itemMenuBirdPrefab;
        [SerializeField] private Button _buttonStart;
        [SerializeField] private Transform _parentBirds;
        [SerializeField] private TextMeshProUGUI _textNameGame;
        [SerializeField] private TextMeshProUGUI _textScore;

        private List<ItemMenuBird> _listBirds = new();

        private void Start()
        {
            InitializePanelBirds();
            _textScore.text = PlayerPrefs.GetInt(DataGameBird.KEY_QUANTITY_SCORE, 0).ToString();
            _buttonStart.onClick.AddListener(OnClickStart);
        }

        public void InitializePanelBirds()
        {
            foreach (var item in _dataBirds._listBirds)
            {
                ItemMenuBird itemMenu = Instantiate(_itemMenuBirdPrefab, _parentBirds);
                itemMenu.Initialize(item, DeactivateAllBirds);
                _listBirds.Add(itemMenu);
            }
        }

        private void DeactivateAllBirds()
        {
            foreach (var item in _listBirds)            
                item.DeactivateBird();          
        }

        private void OnClickStart()
        {
            SceneManager.LoadScene("Birds");
        }

        private void OnDestroy()
        {
            _buttonStart.onClick.RemoveListener(OnClickStart);
        }
    }
}

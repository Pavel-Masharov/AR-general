using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace AR.Birds
{
    public class CanvasUI : MonoBehaviour
    {
        [SerializeField] private GameObject _cloud;
        [SerializeField] private TextMeshProUGUI _textExample;
        [SerializeField] private ButtonAnswaer _firstButtonallAnswaer, _secondButtonAnswaer, _thirdButtonAnswaer;
        [SerializeField] private GameObject _parentAnswer;
        [SerializeField] private Button _buttonBack;
        [SerializeField] private Button _buttonSing;

        private UnityAction _actionSing;

        public void Start()
        {
            _buttonBack.onClick.AddListener(OnClickButtonBack);
            _buttonSing.onClick.AddListener(OnClickButtonSing);
        }

        public void ShowExample(string example, List<int> asks, UnityAction<int> actionAnswer)
        {
            _textExample.text = example;

            _firstButtonallAnswaer.InitializeButtonAnswaer(asks[0], actionAnswer);
            _secondButtonAnswaer.InitializeButtonAnswaer(asks[1], actionAnswer);
            _thirdButtonAnswaer.InitializeButtonAnswaer(asks[2], actionAnswer);

            _cloud.SetActive(true);
            _parentAnswer.SetActive(true);
        }

        public async void ShowResultMessage(string textResult, int delay)
        {
            _textExample.text = textResult;
            await Task.Delay(delay);

            _cloud.SetActive(false);
            _parentAnswer.SetActive(false);
        }

        private void OnClickButtonBack()
        {
            SceneManager.LoadScene("MainMenuBirds");
        }

        private void OnClickButtonSing()
        {
            _actionSing?.Invoke();
        }

        public void SetActionSing(UnityAction actionSing)
        {
            _actionSing = actionSing;
        }
    }
}

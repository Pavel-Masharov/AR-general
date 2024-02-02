using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace AR.Birds
{
    public class ButtonAnswaer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textAnswaer;
        private UnityAction<int> _actionAnswer;
        private int _answer;

        public void InitializeButtonAnswaer(int answer, UnityAction<int> actionAnswer)
        {
            _answer = answer;
            _textAnswaer.text = answer.ToString();
            _actionAnswer = actionAnswer;
            gameObject.GetComponent<Button>().onClick.AddListener(() => _actionAnswer(_answer));
        }

        private void OnDisable()
        {
            gameObject.GetComponent<Button>().onClick.RemoveListener(() => _actionAnswer(_answer));
        }
    }
}

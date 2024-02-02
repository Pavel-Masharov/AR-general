using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace AR.Birds
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private CanvasUI _canvasUI;
        [SerializeField] private Transform _startPosition;
        [SerializeField] private ParticleSystem _particlesFeed;

        private WaitForSeconds _timeChangeAnim = new WaitForSeconds(5);
        private Coroutine _coroutineCreateAsk;
        private bool _hasAsk = false;
        private int _correctAnswer = 0;

        private void Awake()
        {
            string path = "Birds/" + DataGameBird.NameCurentBird;
            var prefab = Resources.Load(path) as GameObject;
            var birdObject = Instantiate(prefab, transform);
            birdObject.transform.position = _startPosition.position;
            Bird bird = birdObject.GetComponent<Bird>();
            bird.InitializeBird(_particlesFeed);
            _inputController.SetBird(bird);
            _canvasUI.SetActionSing(bird.Sing);

        }

        private void Start()
        {
            _coroutineCreateAsk = StartCoroutine(CreateAsk());
        }

        private IEnumerator CreateAsk()
        {
            while(true)
            {
                yield return _timeChangeAnim;

                if(!_hasAsk)
                {
                    GenerateExample();
                }
            }
        }

        private void GenerateExample()
        {
            _hasAsk = true;

            List<int> allAnswaers = new();
            int firstNumber = Random.Range(3, 99);
            int secondNumber = Random.Range(3, 99);
            int result = firstNumber + secondNumber;
            _correctAnswer = result;
            int firstWrongResult = result - 1;
            int secondtWrongResult = result + 3;
            string example = firstNumber + " + " + secondNumber + " = ?";

            allAnswaers.Add(result);
            allAnswaers.Add(firstWrongResult);
            allAnswaers.Add(secondtWrongResult);
            System.Random RND = new();               
            for (int i = 0; i < allAnswaers.Count; i++)
            {
                int tmp = allAnswaers[0];
                allAnswaers.RemoveAt(0);
                allAnswaers.Insert(RND.Next(allAnswaers.Count), tmp);
            }

            _canvasUI.ShowExample(example, allAnswaers, CheckAnswer);
        }

        private async void CheckAnswer(int answer)
        {
            string result;
            if(_correctAnswer == answer)
            {
                int quantityScore = 10;
                AddScore(quantityScore);
                result = "Great! + " + quantityScore + " score";
            }
            else
            {
                result = "wrong";
            }
            int delay = 2000;
            _canvasUI.ShowResultMessage(result, delay);
            await Task.Delay(delay);

            _hasAsk = false;
        }

        private void AddScore(int score)
        {
            int curScore = PlayerPrefs.GetInt(DataGameBird.KEY_QUANTITY_SCORE, 0);
            curScore += score;
            PlayerPrefs.SetInt(DataGameBird.KEY_QUANTITY_SCORE, curScore);
        }

        private void OnDestroy()
        {
            StopCoroutine(_coroutineCreateAsk);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace AR.Birds
{
    public class Bird : MonoBehaviour
    {
        [SerializeField] private AudioClip _clipSing;
        [SerializeField] private AudioClip _clipFly;

        private Animator _animator;
        private StateBird _stateBird = StateBird.Sit;
        private WaitForSeconds _timeChangeAnim = new WaitForSeconds(4);
        private Coroutine _coroutineRunAnimation;
        private int _animationEatHash = Animator.StringToHash("isEat");
        private int _animationFlyHash = Animator.StringToHash("isFly");
        private int _animationSingHash = Animator.StringToHash("isSing");
        private bool _canMove = true;
        private AreaTap _currentArea = null;
        private ParticleSystem _particlesFeed;

        private List<int> _listAnimations = new()
        {
            Animator.StringToHash("isPreen"),
            Animator.StringToHash("isRuffle"),
            Animator.StringToHash("isWorried"),
            Animator.StringToHash("isWatch")
        };

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _coroutineRunAnimation = StartCoroutine(RunAnimation());
        }

        public void InitializeBird(ParticleSystem particlesFeed)
        {
            _particlesFeed = particlesFeed;
        }


        private IEnumerator RunAnimation()
        {
            while(true)
            {
                yield return _timeChangeAnim;
                if(_stateBird == StateBird.Sit)
                    _animator.SetTrigger(GetRandomAnimation());
            }
        }

        public async void EatAnimation()
        {
            if (_stateBird != StateBird.Sit)
                return;

            _particlesFeed.gameObject.SetActive(true);
            _particlesFeed.gameObject.transform.position = transform.position;
            _particlesFeed.Play();
            _animator.SetTrigger(_animationEatHash);
            await Task.Delay(2000);
            _particlesFeed.gameObject.SetActive(false);
            _particlesFeed.Stop();
        }

        public void MoveToPosition(AreaTap areaTap)
        {
            if (!_canMove || _currentArea == areaTap)
                return;

            _currentArea = areaTap;
            _stateBird = StateBird.Move;
            float duration = 2;
            GameAudioController.instance.PlayOneShotSound(_clipFly);
            _animator.SetBool(_animationFlyHash, true); 
            if (areaTap.GetTypeArea() == TypeArea.Sit)            
                transform.DOMove(areaTap.GetTargetPosition(), duration).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    _animator.SetBool(_animationFlyHash, false);
                    _stateBird = StateBird.Sit;
                    _canMove = true;
                });            
            else           
                transform.DOMove(areaTap.GetTargetPosition(), duration).SetEase(Ease.OutSine).OnComplete(() =>
                {
                    _stateBird = StateBird.Fly;
                    _canMove = true;
                }); 
        }
    
        public void Sing()
        {
            GameAudioController.instance.PlayOneShotSound(_clipSing);
            if (_stateBird == StateBird.Sit)
                _animator.SetTrigger(_animationSingHash);
        }

        private int GetRandomAnimation()
        {
            int index = Random.Range(0, _listAnimations.Count);
            return _listAnimations[index];
        }

        private void OnDestroy()
        {
            StopCoroutine(_coroutineRunAnimation);
        }
    }
}

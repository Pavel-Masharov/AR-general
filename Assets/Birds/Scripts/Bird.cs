using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

namespace AR.Birds
{
    public class Bird : MonoBehaviour
    {
        private Animator _animator;
        private AudioSource _sourceAudio;
       // private int _animationIdleHash = Animator.StringToHash("isIdle");
        private int _animationEatHash = Animator.StringToHash("isEat");
        private int _animationFlyHash = Animator.StringToHash("isFly");
        private int _animationSingHash = Animator.StringToHash("isSing");


        [SerializeField] private Transform transformPosUp;
        [SerializeField] private Transform transformPosLeft;
        [SerializeField] private Transform transformPosRight;

        [SerializeField] private ParticleSystem _particlesFeed;

        [SerializeField] private AudioClip _clipSing;
        [SerializeField] private AudioClip _clipFly;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _sourceAudio = GetComponent<AudioSource>();

            transform.SetParent(null);
        }
        public async void EatAnimation()
        {
            _particlesFeed.gameObject.SetActive(true);
            _particlesFeed.Play();
            _animator.SetTrigger(_animationEatHash);
            await Task.Delay(2000);
            _particlesFeed.gameObject.SetActive(false);
            _particlesFeed.Stop();

        }

        public void MoveToPosition(float posY = 0)
        {
            Vector3 posMove = posY != 0 ? new Vector3(transform.position.x, posY, transform.position.z) : transformPosUp.position;

            _sourceAudio.PlayOneShot(_clipFly);
            _animator.SetBool(_animationFlyHash, true);
            float duration = 2;
            transform.DOMove(posMove, duration).OnComplete(()=> _animator.SetBool(_animationFlyHash, false));
        }

        public void Sing()
        {
            _animator.SetTrigger(_animationSingHash);
            _sourceAudio.PlayOneShot(_clipSing);
        }
    }
}

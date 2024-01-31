using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        }
        public void EatAnimation()
        {
            _particlesFeed.gameObject.SetActive(true);
            _particlesFeed.Play();
            _animator.SetTrigger(_animationEatHash);
        }

        public void MoveToPosition(Transform targetPosition)
        {
            _sourceAudio.PlayOneShot(_clipFly);

            _animator.SetBool(_animationFlyHash, true);

            float duration = 2;
            transform.DOMove(transformPosUp.position, duration).OnComplete(()=> _animator.SetBool(_animationFlyHash, false));
        }

        public void Sing()
        {
            _animator.SetTrigger(_animationSingHash);
            _sourceAudio.PlayOneShot(_clipSing);
        }
    }
}

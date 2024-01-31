using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace AR.Birds
{
    public class Bird : MonoBehaviour
    {
        private Animator _animator;

       // private int _animationIdleHash = Animator.StringToHash("isIdle");
        private int _animationEatHash = Animator.StringToHash("isEat");
        private int _animationFlyHash = Animator.StringToHash("isFly");


        [SerializeField] private Transform transformPosUp;
        [SerializeField] private Transform transformPosLeft;
        [SerializeField] private Transform transformPosRight;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        public void EatAnimation()
        {
            _animator.SetTrigger(_animationEatHash);
        }

        public void MoveToPosition(Transform targetPosition)
        {
            _animator.SetBool(_animationFlyHash, true);

            float duration = 3;
            transform.DOMove(transformPosUp.position, duration).OnComplete(()=> _animator.SetBool(_animationFlyHash, false));
        }
    }
}

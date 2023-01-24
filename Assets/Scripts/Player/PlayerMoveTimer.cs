using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMoveTimer : MonoBehaviour
    {
        public UnityEvent TimeIsOver;
        
        [SerializeField]
        private float _timerDuration;

        [SerializeField]
        private Vector3 _finishTimerScale;
        
        private Sequence _timerSequence;
        private Vector3 _startTimerScale;

        private void Start()
        {
            _startTimerScale = transform.localScale;

            StartTimerSequence();
        }

        private void StartTimerSequence()
        {
            _timerSequence = DOTween.Sequence();
            _timerSequence.SetAutoKill(false);
            _timerSequence.Append(transform.DOScale(_startTimerScale, 0f));
            _timerSequence.Append(transform.DOScale(_finishTimerScale, _timerDuration));
            _timerSequence.OnComplete(() => TimeIsOver.Invoke());
        }

        private void OnDestroy()
        {
            _timerSequence.Kill();
        }
    }
}
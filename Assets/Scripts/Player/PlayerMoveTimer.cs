using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Player
{
    public class PlayerMoveTimer : MonoBehaviour
    {
        public event Action TimeIsOver;

        [SerializeField]
        private Transform _timer;
        
        [SerializeField]
        private float _timerDuration;

        [SerializeField]
        private Vector3 _finishTimerScale;
        
        private Sequence _timerSequence;
        private Vector3 _startTimerScale;


        private void Awake()
        {
            _startTimerScale = _timer.localScale;
            StartTimerSequence();
        }

        private void StartTimerSequence()
        {
            _timerSequence = DOTween.Sequence();
            _timerSequence.SetAutoKill(false);
            _timerSequence.Append(_timer.DOScale(_startTimerScale, 0f));
            _timerSequence.Append(_timer.DOScale(_finishTimerScale, _timerDuration));
            _timerSequence.OnComplete(() => TimeIsOver?.Invoke());
        }

        public void RestartTimer()
        {
            _timerSequence.Restart();
        }

        public void StopTimer()
        {
            _timerSequence.Pause();
            _timer.localScale = Vector3.zero;
        }

        private void OnDestroy()
        {
            _timerSequence.Kill();
        }
    }
}
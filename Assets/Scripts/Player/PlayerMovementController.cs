using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField]
        private float _movementVelocity;
        
        [SerializeField]
        private float _duration;

        [SerializeField]
        private float _minRotationAngle;
        
        [SerializeField]
        private float _maxRotationAngle;

        private Rigidbody2D _rigidbody;
        private Vector3 _startPosition;
        private Quaternion _quaternionMinRotationAngle;
        private Quaternion _quaternionMaxRotationAngle;
        private float _currentTime;

        private bool _isMoving;
        private bool _canRotate;
        

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _quaternionMinRotationAngle = Quaternion.Euler(0f,0f, _minRotationAngle);
            _quaternionMaxRotationAngle = Quaternion.Euler(0f,0f, _maxRotationAngle);

            _startPosition = transform.position;
            _isMoving = false;
            _canRotate = true;
        }

        // when player returns to start point
        [UsedImplicitly]
        public void ResetPosition()
        {
            if (!_isMoving) return;
            
            _isMoving = !_isMoving;
            _canRotate = true;
            _rigidbody.velocity = Vector2.zero;
            transform.position = _startPosition;

        }

        // from event after player collision with enemy
        [UsedImplicitly]
        public void ChangeDirection()
        {
            _rigidbody.velocity *= -1;
        }

        public void Move()
        {
            if (_isMoving) return;
            
            _isMoving = !_isMoving;
            _canRotate = false;
            _rigidbody.velocity = transform.up * _movementVelocity;

        }
        
        private void Update()
        {
            if (_canRotate)
            {
                Rotate();
            }
            
        }
        
        private void Rotate()
        {
            _currentTime += Time.deltaTime;
            var progress = Mathf.PingPong(_currentTime, _duration) / _duration;
            transform.rotation = Quaternion.Lerp(_quaternionMinRotationAngle, _quaternionMaxRotationAngle, progress);
        }
    }
}

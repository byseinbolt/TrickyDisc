using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovementController : MonoBehaviour
    {
        public bool CanRotate { get; private set; }

        [SerializeField]
        private float _movementVelocity;
        
        [SerializeField]
        private float _duration;

        [SerializeField]
        private float _minRotationAngle;
        
        [SerializeField]
        private float _maxRotationAngle;

        [SerializeField]
        private SpriteRenderer _aimSprite;

        private Rigidbody2D _rigidbody;
        private Vector3 _startPosition;
        
        private Quaternion _quaternionMinRotationAngle;
        private Quaternion _quaternionMaxRotationAngle;
        
        private float _currentTime;
        private bool _isMoving;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            _quaternionMinRotationAngle = Quaternion.Euler(0f,0f, _minRotationAngle);
            _quaternionMaxRotationAngle = Quaternion.Euler(0f,0f, _maxRotationAngle);

            _startPosition = transform.position;
            _isMoving = false;
            CanRotate = true;
        }
        public void Rotate()
        {
            _currentTime += Time.deltaTime;
            var progress = Mathf.PingPong(_currentTime, _duration) / _duration;
            transform.rotation = Quaternion.Lerp(_quaternionMinRotationAngle, _quaternionMaxRotationAngle, progress);
        }
        
        public void Move()
        {
            if (_isMoving) return;
            
            _isMoving = !_isMoving;
            CanRotate = false;
            _aimSprite.enabled = false;
            
            _rigidbody.velocity = transform.up * _movementVelocity;
        }
        
        public void ResetPosition()
        {
            if (!_isMoving) return;
            
            _isMoving = !_isMoving;
            CanRotate = true;
            _aimSprite.enabled = true;
            _rigidbody.velocity = Vector2.zero;
            transform.position = _startPosition;
        }
        
        public void ChangeDirection()
        {
            _rigidbody.velocity *= -1;
        }
    }
}

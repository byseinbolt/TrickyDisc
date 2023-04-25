using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CollisionController))]
    [RequireComponent(typeof(PlayerMoveTimer))]
    [RequireComponent(typeof(PlayerMovementController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Effects and Sound")]
        [SerializeField]
        private AudioSource _moveSound;

        [SerializeField]
        private ParticleSystem _deathEffect;

        private CollisionController _collisionController;
        private PlayerMoveTimer _moveTimer;
        private PlayerMovementController _movementController;
        private InputActions _playerInputActions;

        private void Awake()
        {
            _movementController = GetComponent<PlayerMovementController>();
            _moveTimer = GetComponent<PlayerMoveTimer>();
            _collisionController = GetComponent<CollisionController>();
            _playerInputActions = new InputActions();

            _playerInputActions.Player.Move.performed += OnMove;
            _moveTimer.TimeIsOver += _movementController.Move;
            _collisionController.EnemyDestroyed += _movementController.ChangeDirection;
            _collisionController.PlayerCameToStart += ResetPosition;
            _collisionController.PlayerDied += OnDied;
        }

        private void OnEnable()
        {
            _playerInputActions.Enable();
        }

        private void Update()
        {
            if (_movementController.CanRotate)
            {
                _movementController.Rotate();
            }
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            Move();
        }

        private void ResetPosition()
        {
            _movementController.ResetPosition();
            _moveTimer.RestartTimer();
        }

        private void Move()
        {
            _movementController.Move();
            _moveTimer.StopTimer();
            _moveSound.Play();
        }

        private void OnDied()
        {
            var deathParticle = Instantiate(_deathEffect, transform.position, quaternion.identity);
            Destroy(gameObject);
            Destroy(deathParticle.gameObject, 2f);
        }

        private void OnDisable()
        {
            _playerInputActions.Disable();
        }

        private void OnDestroy()
        {
            _playerInputActions.Player.Move.performed -= OnMove;
            _moveTimer.TimeIsOver -= _movementController.Move;
            _collisionController.EnemyDestroyed -= _movementController.ChangeDirection;
            _collisionController.PlayerCameToStart -= _movementController.ResetPosition;
            _collisionController.PlayerDied -= OnDied;
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CollisionController))]
    [RequireComponent(typeof(PlayerMoveTimer))]
    [RequireComponent(typeof(PlayerMovementController))]
    public class PlayerController : MonoBehaviour
    {
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
        }

        private void OnDied()
        {
            Destroy(gameObject);
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
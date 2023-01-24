using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
   public class PlayerInput : MonoBehaviour
   {
      [SerializeField]
      private PlayerMovementController _playerMovement;
      
      private InputActions _playerInputActions;
      private void Awake()
      {
         _playerInputActions = new InputActions();
         _playerInputActions.Player.Move.performed += OnMove;
      }
   
      private void OnEnable()
      {
         _playerInputActions.Enable();
      }
   
      private void OnMove(InputAction.CallbackContext obj)
      {
         _playerMovement.Move();
      }

      private void OnDisable()
      {
         _playerInputActions.Disable();
      }

      private void OnDestroy()
      {
         _playerInputActions.Player.Move.performed -= OnMove;
      }
   }
}

using Game;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class PlayerCollisionController : MonoBehaviour
    {
        public UnityEvent EnemyDestroyed;
        public UnityEvent PlayerCameToStart;
        
        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.CompareTag(GlobalConstants.BORDER_TAG))
            {
                Debug.Log("player died");
            }

            if (otherCollider.CompareTag(GlobalConstants.ENEMY_TAG))
            {
                EnemyDestroyed.Invoke();
            }

            if (otherCollider.CompareTag(GlobalConstants.START_POINT_TAG))
            {
                PlayerCameToStart.Invoke();
            }
        }
    }
}
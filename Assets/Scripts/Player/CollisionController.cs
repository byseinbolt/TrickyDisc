using System;
using Enemy;
using Game;
using UnityEngine;
using UnityEngine.Events;

namespace Player
{
    public class CollisionController : MonoBehaviour
    {
        public event Action EnemyDestroyed;
        public event Action PlayerCameToStart;

        public event Action PlayerDied;
        
        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.CompareTag(GlobalConstants.BORDER_TAG))
            {
                PlayerDied?.Invoke();
            }

            if (otherCollider.CompareTag(GlobalConstants.ENEMY_TAG))
            {
                otherCollider.TryGetComponent(out EnemyController controller);
                if (controller!=null)
                {
                    controller.Destroy();
                    EnemyDestroyed?.Invoke();
                }
            }

            if (otherCollider.CompareTag(GlobalConstants.START_POINT_TAG))
            {
                PlayerCameToStart?.Invoke();
            }
        }
    }
}
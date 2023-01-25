using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField]
        private float _minMovingDuration;

        [SerializeField]
        private float _maxMovingDuration;

        [SerializeField]
        private float _delayBetweenMovements;

        [Header("Effects and Sound")]
        [SerializeField]
        private ParticleSystem _deathEffect;
        
        private float _minPointX;
        private float _maxPointX;
        private SpriteRenderer _enemySprite;
        private Sequence _moveSequence;
        

        private void Awake()
        {
            _enemySprite = GetComponent<SpriteRenderer>();
            var offsetX = _enemySprite.bounds.size.x / 2;
            var mainCamera = Camera.main;
            _minPointX = mainCamera!.ViewportToWorldPoint(Vector3.zero).x + offsetX;
            _maxPointX = mainCamera.ViewportToWorldPoint(new Vector3(1f, 0f, 0f)).x - offsetX;
        }

       
        
        public void Move()
        {
            var duration = GetRandomDuration();
            var position = GetRandomPosition();
            
             _moveSequence = DOTween.Sequence();
            _moveSequence.Append(transform.DOMoveX(position, duration));
            _moveSequence.AppendInterval(_delayBetweenMovements)
                .OnComplete(Move);

        }

        public void Destroy()
        {
            var deathParticle = Instantiate(_deathEffect, transform.position, quaternion.identity);
            Destroy(gameObject);
            Destroy(deathParticle.gameObject, 2f);
        }

        private float GetRandomDuration()
        {
            return Random.Range(_minMovingDuration, _maxMovingDuration);
        }

        private float GetRandomPosition()
        {
            return Random.Range(_minPointX, _maxPointX);
        }

        private void OnDestroy()
        {
            _moveSequence.Kill();
        }
    }
}

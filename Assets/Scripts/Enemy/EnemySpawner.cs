using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private Transform _leftSpawnPoint;
        
        [SerializeField]
        private Transform _rightSpawnPoint;

        [SerializeField]
        private EnemyController _enemyPrefab;
        
        private void Start()
        {
            Spawn();
        }

        public void Spawn()
        {
            var spawnPosition = ShouldSpawnOnTheLeftSide() ? _leftSpawnPoint.position : _rightSpawnPoint.position;
            var enemy = Instantiate(_enemyPrefab, spawnPosition, quaternion.identity);
            enemy.Move();
        }
        
        private bool ShouldSpawnOnTheLeftSide()
        {
            return Random.Range(0, 2) == 1;
        }
    }
}
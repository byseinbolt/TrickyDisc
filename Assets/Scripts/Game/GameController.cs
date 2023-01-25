using System.Collections;
using Enemy;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private EnemySpawner _enemySpawner;
        
        [SerializeField]
        private CollisionController _collisionController;

        [SerializeField]
        private ScoreController _scoreController;

        [SerializeField]
        private float _delayBeforeChangeScene;
        
        private void Start()
        {
            _collisionController.EnemyDestroyed += _enemySpawner.Spawn;
            _collisionController.EnemyDestroyed += _scoreController.AddScore;
            _collisionController.PlayerDied += OnPlayerDied;
        }

        private void OnPlayerDied()
        {
            StartCoroutine(ShowGameOver());
        }
        private void OnDestroy()
        {
            _collisionController.EnemyDestroyed -= _enemySpawner.Spawn;
            _collisionController.EnemyDestroyed -= _scoreController.AddScore;
            _collisionController.PlayerDied -= OnPlayerDied;

        }

        private IEnumerator ShowGameOver()
        {
            yield return new WaitForSeconds(_delayBeforeChangeScene);
            SceneManager.LoadSceneAsync(GlobalConstants.GAMEOVER_SCENE);
        }
        
    }
}
using System;
using DG.Tweening;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _scoreLabel;

        [SerializeField]
        private TextMeshProUGUI _bestScoreLabel;

        [SerializeField]
        private float _newBestScoreAnimationDuration;

        private void Awake()
        {
            var currentScore = PlayerPrefs.GetInt(GlobalConstants.SCORE_PREFS_KEY);
            var bestScore = PlayerPrefs.GetInt(GlobalConstants.BEST_SCORE_PREFS_KEY);

            if (currentScore>bestScore)
            {
                bestScore = currentScore;
                SetNewBestScore(bestScore);
                _bestScoreLabel.transform.DOPunchScale(Vector3.one, _newBestScoreAnimationDuration, 0);
            }

            _scoreLabel.text = currentScore.ToString();
            _bestScoreLabel.text = $"BEST {bestScore.ToString()}";
        }
        
        // from restart button
        [UsedImplicitly]
        public void RestartGame()
        {
            SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
        }

        //from exit button
        [UsedImplicitly]
        public void ExitToStartGameScreen()
        {
            SceneManager.LoadSceneAsync(GlobalConstants.START_GAME_SCENE);
        }

        private void SetNewBestScore(int newBestScore)
        {
            PlayerPrefs.SetInt(GlobalConstants.BEST_SCORE_PREFS_KEY, newBestScore);
            PlayerPrefs.Save();
        }
    }
}

using DG.Tweening;
using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreController : MonoBehaviour
    {
        [Header("View")]
        [SerializeField]
        private TextMeshProUGUI _scoreLabel;

        [SerializeField]
        private int _scoreCountPerEnemy;

        [Header("Animation")]
        [SerializeField]
        private float _animationDuration;

        [SerializeField]
        private float _scaleFactor;

        [Header("Sound")]
        [SerializeField] 
        private AudioSource _changeScoreSound;


        private int _currentScore;

        private void Awake()
        {
            _scoreLabel.text = "0";
        }

        public void AddScore()
        {
            _currentScore += _scoreCountPerEnemy;
            _scoreLabel.text = _currentScore.ToString();
            _changeScoreSound.Play();
            _scoreLabel.transform.DOPunchScale(Vector3.one * _scaleFactor, _animationDuration, 0)
                .OnComplete(() => _scoreLabel.transform.localScale = Vector3.one);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetInt(GlobalConstants.SCORE_PREFS_KEY, _currentScore);
            PlayerPrefs.Save();
        }
    }
}

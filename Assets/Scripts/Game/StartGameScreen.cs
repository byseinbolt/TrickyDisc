using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class StartGameScreen : MonoBehaviour
    {
        // from start button 
        [UsedImplicitly]
        public void StartGame()
        {
            SceneManager.LoadSceneAsync(GlobalConstants.GAME_SCENE);
        }

        // from exit button
        [UsedImplicitly]
        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}

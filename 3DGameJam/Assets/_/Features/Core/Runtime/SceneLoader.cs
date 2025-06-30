using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Runtime
{
    public class SceneLoader : BaseMonobehaviour
    {

        #region Publics

        public static event Action<Scene> OnSceneLoaded;
        public static string CurrentSceneName => SceneManager.GetActiveScene().name;

        #endregion


        #region Unity API

        private void Awake()
        {
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }

        void OnDestroy()
        {
            SceneManager.sceneLoaded -= HandleSceneLoaded;
        }

        #endregion


        #region Main Methods

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        #endregion


        #region Utils

        private void HandleSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            OnSceneLoaded?.Invoke(scene);
        }
        
        #endregion


        #region Privates and Protected

        // Variables privées

        #endregion
    }
}


using Core.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace AudioSystem.Runtime
{
    public class LevelAudioHandler : MonoBehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void OnEnable()
        {
            SceneLoader.OnSceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneLoader.OnSceneLoaded -= OnSceneLoaded;
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */
        private void OnSceneLoaded(Scene scene)
        {
            if (scene.name == SceneLoader.CurrentSceneName)
            {
                AudioManager.Instance.PlayLevelMusic();
                AudioManager.Instance.SetMusicVolume(0.2f);
            }
        }

        #endregion


        #region Privates and Protected

        // Variables privées

        #endregion
    }
}


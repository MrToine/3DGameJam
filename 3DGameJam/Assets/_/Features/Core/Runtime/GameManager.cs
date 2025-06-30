using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Core.Runtime
{
    [RequireComponent(typeof(SceneLoader))]
    public class GameManager: BaseMonobehaviour
    {

        #region Publics

        public static GameManager Instance { get; private set; }
        
        public bool IsOnPause
        {
            get => isOnPause;
            set => isOnPause = value;
        }

        #endregion


        #region Unity API

        private void Awake()
        {
            _sceneLoader = GetComponent<SceneLoader>();
            // Si une instance existe déjà et que ce n’est pas celle-ci, détruire ce GameObject
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // Sinon, cette instance devient l’instance unique
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        #endregion


        #region Main Methods
        
        public void CreateObject(GameObject prefab, Vector3 position)
        {
            Instantiate(prefab, position, Quaternion.identity);
        }

        public void ReloadScene()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            _sceneLoader.LoadScene(sceneName);
        }
        
        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        private bool isOnPause = false;
        SceneLoader _sceneLoader;

        #endregion


    }
}
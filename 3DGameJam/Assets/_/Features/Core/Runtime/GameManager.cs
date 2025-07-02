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
        public static event Action OnGameOver;
        public UnityEvent OnBattleAreaEnd;
        
        public bool IsOnPause
        {
            get => isOnPause;
            set => isOnPause = value;
        }
        
        public bool IsOnGameOver
        {
            get => isOnGameOver;
            set => isOnGameOver = value;
        }
        
        public bool BattleAreaEnd
        {
            get => isBattleAreaEnd;
            set => isBattleAreaEnd = value;
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

        private void Update()
        {
            if (isOnGameOver)
            {
                IsGameOver();
            }

            if (BattleAreaEnd)
            {
                OnBattleAreaEnd?.Invoke();
            }
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
        
        public void LoadScene(string sceneName)
        {
            Info("On charge un scene grâce au FakeLoading");
            _sceneLoader.FakeLoading(sceneName);
        }
        
        #endregion


        #region Utils

        /* Fonctions privées utiles */
        private void IsGameOver()
        {
            OnGameOver?.Invoke();
        }

        #endregion


        #region Privates and Protected

        private bool isOnPause = false;
        private bool isOnGameOver = false;
        private bool isBattleAreaEnd = false;
        
        SceneLoader _sceneLoader;

        #endregion


    }
}
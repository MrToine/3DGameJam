using System;
using System.Collections;
using System.Collections.Generic;
using Core.Runtime;
using TreeEditor;
using Unity.Cinemachine;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;
using WaveSystem.Runtime;
using Random = UnityEngine.Random;

namespace CombatArea.Runtime
{
    public class CombatAreaBehaviour : MonoBehaviour
    {
        public int m_dollySpeed;

        private int _currentWaveIndex;
        private bool _waveCleared;
        private bool _hasBeenSpawned;
        
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private CinemachineCamera _currentCamera;
        [SerializeField] private GameObject _gunPrefab;
        [SerializeField] private CinemachineSplineCart _splineContainer;

        private List<GameObject> _currentWaveEnemies = new List<GameObject>();

        private void Awake()
        {
            GameManager.Instance.OnBattleAreaEnd.AddListener(KeepGoing);
        }

        void KeepGoing()
        {
            if (_splineContainer.AutomaticDolly.Method is SplineAutoDolly.FixedSpeed autodolly)
            {
                Debug.Log("In Dolly  ?");

                autodolly.Speed = m_dollySpeed;
                _currentCamera.Priority = 2;
                _camera.Priority = 1;
                _hasBeenSpawned = false;
                _gunPrefab.SetActive(false);

            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("In ?");
            if (other.CompareTag("Player") && !_hasBeenSpawned)
            {
                Debug.Log("In  PLayer ??");

                if (_splineContainer.AutomaticDolly.Method is SplineAutoDolly.FixedSpeed autodolly)
                {
                    Debug.Log("In Dolly  ?");

                    autodolly.Speed = 0;
                    _currentCamera.Priority = 1;
                    _camera.Priority = 2;
                    _hasBeenSpawned = true;
                    _gunPrefab.SetActive(true);

                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")  && _hasBeenSpawned)
            {
                _hasBeenSpawned = false;
            }
        }
        private void Update()
        {
            if (_waveCleared == false)
            {
                //CheckEnemies();
            }
        }
        
        [ContextMenu("Check Enemies")]
        private void CheckEnemies()
        {
            //
        }
    }
}

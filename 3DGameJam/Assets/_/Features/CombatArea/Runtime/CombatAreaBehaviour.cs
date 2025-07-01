using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Cinemachine;
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
        
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private CinemachineCamera _currentCamera;
        [SerializeField] private GameObject _gunPrefab;
        [SerializeField] private CinemachineSplineCart _splineContainer;

        private List<GameObject> _currentWaveEnemies = new List<GameObject>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_splineContainer.AutomaticDolly.Method is SplineAutoDolly.FixedSpeed autodolly)
                {
                    autodolly.Speed = 0;
                    _currentCamera.Priority = 1;
                    _camera.Priority = 2;
                    _gunPrefab.SetActive(true);

                }
            }
        }

        private void Update()
        {
            if (_waveCleared == true)
            {
                CheckEnemies();
            }
        }
        
        [ContextMenu("Check Enemies")]
        private void CheckEnemies()
        {
            //
        }
    }
}

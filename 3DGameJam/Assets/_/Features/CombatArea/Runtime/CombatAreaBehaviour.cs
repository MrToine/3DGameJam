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

        public List<Wave> m_enemiesWaves = new List<Wave>();
        public List<Transform> m_enemiesSpawns = new List<Transform>();
        public UnityEvent m_waveCleared;
        public int m_dollySpeed;

        private int _currentWaveIndex;
        private bool _waveCleared;
        private bool _hasBeenSpawned;
        
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private CinemachineCamera _currentCamera;
        [SerializeField] private GameObject _gunPrefab;
        [SerializeField] private CinemachineSplineCart _splineContainer;

        private List<GameObject> _currentWaveEnemies = new List<GameObject>();
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
                    SpawnEnemies();

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

        private void SpawnEnemies()
        {
            if (_currentWaveIndex == 20) return;
            
            for(int g = _currentWaveIndex; g < m_enemiesWaves.Count; g++)
            {
                _currentWaveIndex = g;
                Wave wave = m_enemiesWaves[g];
                StartCoroutine(SpawnWaveCoroutine(wave));
                Debug.Log("_currentWaveIndex: " + _currentWaveIndex + "g : " + g );
            }
        }

        private IEnumerator SpawnWaveCoroutine(Wave wave)
        { 
            _currentWaveEnemies.Clear();
            foreach (var type in wave.m_enemyTypes)
            {
                for (int i = 0; i < type.m_count; i++)
                {
                    var rng = Random.Range(0, m_enemiesSpawns.Count);
                    var go = Instantiate(type.m_prefab, m_enemiesSpawns[rng].transform.position, Quaternion.identity);
                    _currentWaveEnemies.Add(go);
                    yield return new WaitForSeconds(0.5f); // délai entre chaque spawn
                }
            }
        }
        
        [ContextMenu("Check Enemies")]
        private void CheckEnemies()
        {
            if (_currentWaveIndex >= m_enemiesWaves.Count-1)
            {
                if (_splineContainer.AutomaticDolly.Method is SplineAutoDolly.FixedSpeed autodolly)
                {
                    
                    autodolly.Speed = m_dollySpeed;
                    _gunPrefab.SetActive(false);
                    _currentCamera.Priority = 2;
                    _camera.Priority = 1;

                }
                m_waveCleared.Invoke();
                _waveCleared = true;
            }
            else
            {
                SpawnEnemies();
                
            }
        }
    }
}

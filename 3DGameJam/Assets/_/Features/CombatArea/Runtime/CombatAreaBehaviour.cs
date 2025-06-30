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
        
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private CinemachineCamera _currentCamera;
        [SerializeField] private GameObject _gunPrefab;
        [SerializeField] private CinemachineSplineCart _splineContainer;

        private List<GameObject> _currentWaveEnemies;
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
                    SpawnEnemies();

                }
            }
        }

        private void SpawnEnemies()
        {
            //if (_currentWaveEnemies.Count <= 0) return;
           for(int g = 0; g < m_enemiesWaves.Count; g++){
             var wave = m_enemiesWaves[g];
             StartCoroutine(SpawnWaveCoroutine(wave));

           }
        }

        private IEnumerator SpawnWaveCoroutine(Wave wave)
        {
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
            List<GameObject> enemies = new List<GameObject>();
            if (enemies.Count == 0)
            {
                if (_splineContainer.AutomaticDolly.Method is SplineAutoDolly.FixedSpeed autodolly)
                {
                    
                    autodolly.Speed = m_dollySpeed;
                    _gunPrefab.SetActive(false);
                    _currentCamera.Priority = 2;
                    _camera.Priority = 1;

                }
                m_waveCleared.Invoke();
            }
        }
    }
}

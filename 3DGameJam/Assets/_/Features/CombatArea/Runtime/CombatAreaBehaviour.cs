using System;
using System.Collections.Generic;
using TreeEditor;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;
using WaveSystem.Runtime;

namespace CombatArea.Runtime
{
    public class CombatAreaBehaviour : MonoBehaviour
    {

        public List<Wave> m_enemiesWaves = new List<Wave>();
        public UnityEvent m_waveCleared;
        public int m_dollySpeed;
        
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private CinemachineCamera _currentCamera;
        [SerializeField] private GameObject _gunPrefab;
        [SerializeField] private CinemachineSplineCart _splineContainer;
        
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

        private void SpawnEnemies()
        {
           for(int g = 0; g < m_enemiesWaves.Count; g++){
             var wave = m_enemiesWaves[g];
                /*for(var j = 0; j <  wave.m_enemyTypes.Count; j++)
                {
                    var type = ;
                    for (var i = 0; i < type.m_enemyTypes[j].m_count;)
                    {
                        
                        Debug.Log("Counter " + i);
                        if (i >= 20) return;
                        var go = Instantiate(enemyType.m_prefab,transform.position,Quaternion.identity);
                        i++;

                    }

                }*/

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

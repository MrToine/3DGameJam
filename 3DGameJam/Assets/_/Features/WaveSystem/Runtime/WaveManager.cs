using System.Collections.Generic;
using UnityEngine;
using Core.Runtime;
using PoolSystem.Runtime;
using System.Collections;

namespace WaveSystem.Runtime
{
    public class WaveManager : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void OnEnable()
        {
            _poolSystem = GetComponent<PoolSystem.Runtime.PoolSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            StartCoroutine(HandleWave());
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        [ContextMenu("Démarrer la vague")]
        public void StartWave()
        {
            StartCoroutine(HandleWave());
        }

        private IEnumerator HandleWave()
        {
            _activeEnemies.Clear();

            foreach (var wave in _waves)
            {
                yield return StartCoroutine(HandleSingleWave(wave));
            }
        }

        private IEnumerator HandleSingleWave(WaveData wave)
        {
            foreach (var enemySpawnData in wave.enemiesToSpawn)
            {
                for (int i = 0; i < enemySpawnData.enemyCount; i++)
                {
                    Transform spawnPoint = null;
                    const float checkRadius = 1.0f;
                    int maxTries = 10;
                    int tries = 0;

                    while (tries < maxTries)
                    {
                        var candidate = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                        if (!Physics.CheckSphere(candidate.position, checkRadius))
                        {
                            spawnPoint = candidate;
                            break;
                        }
                        tries++;
                    }

                    if (spawnPoint == null)
                    {
                        Info("No available spawn point found. Spawning anyway.");
                        spawnPoint = _spawnPoints[0];
                    }

                    var enemy = Instantiate(enemySpawnData.enemyPrefab, spawnPoint.position, Quaternion.identity);
                    _activeEnemies.Add(enemy);
                    yield return new WaitForSeconds(enemySpawnData.spawnDelay);
                }
            }
            yield return new WaitUntil(AllEnemiesCleared);
        }

        private bool AllEnemiesCleared()
        {
            foreach (var enemy in _activeEnemies)
            {
                if (enemy is not null && enemy.activeInHierarchy)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion


        #region Privates and Protected

        [Header("Numbers of waves")]
        [SerializeField] private WaveData[] _waves;
        
        //[Header("Spawns points")]
        [SerializeField] private List<Transform> _spawnPoints;
        
        private PoolSystem.Runtime.PoolSystem _poolSystem;
        private readonly List<GameObject> _activeEnemies = new List<GameObject>();

        #endregion
    }
}

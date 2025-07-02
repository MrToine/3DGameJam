using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Runtime;
using PoolSystem.Runtime;
using System.Collections;
using Random = UnityEngine.Random;

namespace WaveSystem.Runtime
{
    public class WaveManager : BaseMonobehaviour
    {

        #region Publics

        public bool HasTriggered
        {
            get { return _hasTriggered; }
            set { _hasTriggered = value; }
        }

        #endregion


        #region Unity API

        private void OnEnable()
        {
            _poolSystem = GetComponent<PoolSystem.Runtime.PoolSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_hasTriggered)
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    _hasTriggered = true;
                    StartCoroutine(HandleWave());
                }
            }
        }
        
        private void Update()
        {
            if (_countWaves >= _waves.Length)
            {
                GameManager.Instance.BattleAreaEnd = true;
                enabled = false;
            }
        }

        #endregion


        #region Main Methods

        public void LaunchWaves()
        {
            _hasTriggered = true;
            StartCoroutine(HandleWave());
        }
        
        #endregion


        #region Utils

        private IEnumerator HandleWave()
        {
            _activeEnemies.Clear();

            foreach (var wave in _waves)
            {
                yield return StartCoroutine(HandleSingleWave(wave));
            }
            yield return new WaitForSeconds(1f);
            GameManager.Instance.BattleAreaEnd = false;
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
            yield return new WaitForSeconds(1f);
            
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
            _countWaves++;
            Info("Wave cleared");
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
        private int _countWaves = 0;
        private bool _hasTriggered = false;

        #endregion
    }
}

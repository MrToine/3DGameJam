using System.Collections.Generic;
using UnityEngine;

namespace WaveSystem.Runtime
{
    [System.Serializable]
    public class EnemySpawnData
    {
        public GameObject enemyPrefab;
        public int enemyCount = 1;
        public float spawnDelay = 0.5f; // délai entre chaque ennemi de ce type
    }
}

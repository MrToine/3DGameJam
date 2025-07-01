using System.Collections.Generic;
using UnityEngine;

namespace WaveSystem.Runtime
{
    [CreateAssetMenu(fileName = "NewWaveData", menuName = "Game/WaveData")]
    public class WaveData : ScriptableObject
    {
        public List<EnemySpawnData> enemiesToSpawn;
        public float timeBeforeNextWave = 2f;
    }

}


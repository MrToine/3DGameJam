using System.Collections.Generic;
using UnityEngine;

namespace WaveSystem.Runtime
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject m_prefab;
        public int m_count;
    }
    public class Wave : MonoBehaviour
    {
        public List<EnemyType> m_enemyTypes = new List<EnemyType>();
    }

}

using UnityEngine;

namespace Character.SO
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Datas/Enemy Datas")]
    public class EnemyData : SOStat
    {
        
        [Header("Données Ennemie")]
        public EnemyType enemyType;
        public GameObject bulletPrefab;
    }

    public enum EnemyType
    {
        Basic,
        Elite,
    }
}

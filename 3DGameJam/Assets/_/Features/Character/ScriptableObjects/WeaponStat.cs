using UnityEngine;

namespace Character.SO
{
    [CreateAssetMenu(fileName = "WeaponStat", menuName = "Scriptable Objects/WeaponStat")]
    public class WeaponStat : ScriptableObject
    {
        public int m_damage;
        public float m_radius;
        public int m_magazine;
        public float m_shotGunFallOffDistance;
        public float m_reloadTime;
        
        [Header("Mesh")]
        public Mesh m_mesh;
        public Material m_material;
    }
}

using UnityEngine;

namespace Character.SO
{
    [CreateAssetMenu(fileName = "WeaponStat", menuName = "Scriptable Objects/WeaponStat")]
    public class WeaponStat : ScriptableObject
    {
        public int m_damage;
        public float m_radius;
    }
}

using UnityEngine;

namespace Player.SO
{
    [CreateAssetMenu(fileName = "Stats", menuName = "Datas/Stats")]
    public class SOStat : ScriptableObject
    {
        [Header("Paramètre de santé")]
        public float maxHealth;
        
        [Header("Paramètre d'endurance")]
        public float maxStamina;
        public float staminaRegenRate;
        
        [Header("Paramètre de leveling")]
        public int baseXp;
        public int level;

        [Header("Paramètre de dommages")]
        public float attackPower;
        public float defense;
    }
}

using UnityEngine;

namespace Character.SO
{
    [CreateAssetMenu(fileName = "Stats", menuName = "Datas/Stats")]
    public class SOStat : ScriptableObject
    {
        public StatDatas Stats()
        {
            return new StatDatas
            {
                MaxHealth = maxHealth,
                MaxStamina = maxStamina,
                StaminaRegenRate = staminaRegenRate,
                Level = level,
                AttackPower = attackPower,
                Defense = defense
            };
        }
        
        [Header("Paramètre de santé")]
        public float maxHealth;
        
        [Header("Paramètre d'endurance")]
        public float maxStamina;
        public float staminaRegenRate;
        
        [Header("Paramètre de leveling")]
        public int baseXp;
        public int level;

        [Header("Paramètre de dommages")]
        public int attackPower;
        public float defense;
    }

    public struct StatDatas
    {
        public float MaxHealth;
        public float MaxStamina;
        public float StaminaRegenRate;
        public int BaseXp;
        public int Level;
        public int AttackPower;
        public float Defense;
    }
}

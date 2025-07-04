using Core.Runtime;
using Game.Interfaces;
using Character.SO;
using UnityEngine;

namespace Character.Runtime
{
    public class CharacterStat : BaseMonobehaviour, IStatProvider
    {

        #region Publics
        
        public SOStat CharacterStats => _stats;
        public float MaxHealth => _stats.maxHealth;
        public float CurrentHealth => _currentHealth;
        public float CurrentStamina => _currentStamina;
        public int CurrentXp => _currentXP;
        public int CurrentLevel => _currentLevel;
        public int AttackPower => _attackPower;

        #endregion


        #region Unity API

        private void Awake()
        {
            if (_stats == null)
            {
                Error("CharacterStat : Stats are null");
                return;
            }
            _currentHealth = _stats.maxHealth; 
            _currentStamina = _stats.maxStamina; 
            _currentXP = _stats.baseXp; 
            _currentLevel = _stats.level;
            _attackPower = _stats.attackPower;
        }

        #endregion


        #region Main Methods

        public float GetMaxHealth()
        {
            return _stats.maxHealth;
        }

        public float GetCurrentHealth()
        {
            return _currentHealth;
        }

        public float GetAttackPower()
        {
            return _stats.attackPower;
        }

        public float GetDefense()
        {
            return _stats.defense;
        }

        public int GetLevel()
        {
            return _currentLevel;
        }

        public void TakeDamage(float amount)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - amount);
            // Event ou animation ici
        }

        public void Heal(float amount)
        {  
            _currentHealth = Mathf.Min(_currentHealth + amount, _stats.maxHealth);
            // Event ou animation ici
        }

        public void LevelUp()
        {
            _currentLevel++;
            // Event ou animation ici
        }

        public void TakeXp(int amount)
        {
            _currentXP += amount;
            // Event ou animation ici
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        [SerializeField] private SOStat _stats;
        private float _currentHealth;
        private float _currentStamina;
        private int _currentXP;
        private int _currentLevel;
        private int _attackPower;

        #endregion
        
    }
}


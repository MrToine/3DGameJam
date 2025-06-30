using System;
using Core.Runtime;
using Game.Interfaces;
using Character.Runtime;
using UnityEngine;

namespace HealthSystem.Runtime
{
    [RequireComponent(typeof(CharacterStat))]
    public class Health : BaseMonobehaviour
    {
            #region Publics

            public float CurrentHealth => _statProvider.GetCurrentHealth();
            public float MaxHealth => _statProvider.GetMaxHealth();
            
            public Action OnHealthChange;
            public Action OnDeath;
    
            #endregion


            #region Unity API


            // Start is called once before the first execution of Update after the MonoBehaviour is created
            void Awake()
            {
                _characterStat = GetComponent<CharacterStat>();
                _statProvider = _characterStat as IStatProvider;

                if (_statProvider == null)
                {
                    Error($"HealthSystem: {_characterStat.name} do note has IStatProvider !");
                }
            }
        

            #endregion
    


            #region Main Methods

            public void TakeDamage(float amount)
            {
                if (_statProvider is CharacterStat playerStat)
                {
                    playerStat.TakeDamage(amount);
                    OnHealthChange?.Invoke();

                    if (playerStat.CurrentHealth <= 0)
                    {
                        OnDeath?.Invoke();
                    }
                }
            }

            public void Heal(float amount)
            {
                if (_statProvider is CharacterStat playerStat)
                {
                    playerStat.Heal(amount);
                    if (playerStat.CurrentHealth < playerStat.MaxHealth)
                    {
                        OnHealthChange?.Invoke();
                    }
                }
            }
    
            #endregion

    
            #region Utils
    
            /* Fonctions privées utiles */
    
            #endregion
    
    
            #region Privates and Protected
        
            private CharacterStat _characterStat;
            
            private IStatProvider _statProvider;

            #endregion
    }
}

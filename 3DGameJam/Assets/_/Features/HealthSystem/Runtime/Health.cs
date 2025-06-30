using System;
using Core.Runtime;
using Game.Interfaces;
using Player.Runtime;
using Player.SO;
using UnityEngine;

namespace HealthSystem.Runtime
{
    [RequireComponent(typeof(PlayerStat))]
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
                _playerStat = GetComponent<PlayerStat>();
                _statProvider = _playerStat as IStatProvider;

                if (_statProvider == null)
                {
                    Error($"HealthSystem: {_playerStat.name} do note has IStatProvider !");
                }
            }
        

            #endregion
    


            #region Main Methods

            public void TakeDamage(float amount)
            {
                if (_statProvider is PlayerStat playerStat)
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
                if (_statProvider is PlayerStat playerStat)
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
        
            private PlayerStat _playerStat;
            
            private IStatProvider _statProvider;

            #endregion
    }
}

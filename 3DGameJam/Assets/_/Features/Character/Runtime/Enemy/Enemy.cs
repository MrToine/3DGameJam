using System;
using Character.SO;
using Core.Runtime;
using UnityEngine;

namespace Character.Runtime
{
    public class Enemy : BaseMonobehaviour
    {

        private void Awake()
        {
            _characterStat = GetComponent<CharacterStat>();
            var enemyData = _characterStat.CharacterStats as EnemyData;
            var shooter = GetComponentInChildren<EnemyShooter>();
            if (shooter == null)
            {
                Info("Shooter manquant sur l'ennemi.");
            }
            if (shooter != null && enemyData != null)
            {
                shooter.Init(enemyData.enemyType, enemyData.bulletPrefab, enemyData.Stats());
            }
            else
            {
                Debug.LogWarning("Shooter ou EnemyData manquant sur l'ennemi.");
            }
        }

        private void OnEnable()
        {
            //LookAtPlayer();
        }

        private void Update()
        {
            LookAtPlayer();
            if (_characterStat.CurrentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void LookAtPlayer()
        {
            if (_player == null)
            {
                _player = GameObject.FindWithTag("Player").transform;
            }
            var direction = _player.position - transform.position;
            direction.Normalize();
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
        public void TakeDamage(int amount)
        {
            Info($"Il prend {amount} degats, il lui reste {_characterStat.CurrentHealth} PV");
            _characterStat.TakeDamage(amount);
        }
        
        private Transform _player;
        private CharacterStat _characterStat;
    }
}

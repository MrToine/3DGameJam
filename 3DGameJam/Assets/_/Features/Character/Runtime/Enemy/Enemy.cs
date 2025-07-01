using System;
using Core.Runtime;
using UnityEngine;

namespace Character.Runtime
{
    public class Enemy : BaseMonobehaviour, IDamageable
    {
        private CharacterStat _characterStat;

        private void Awake()
        {
            _characterStat = GetComponent<CharacterStat>();
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
    }
}


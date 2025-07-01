using System;
using Core.Runtime;
using UnityEngine;

namespace Character.Runtime.Player
{
    public class Player : BaseMonobehaviour, IDamageable
    {

        private CharacterStat _characterStat;

        private void Awake()
        {
            _characterStat = GetComponent<CharacterStat>();
        }

        private void Update()
        {
            Info($"Vie actuelle : {_characterStat.CurrentHealth}");
            if (_characterStat.CurrentHealth <= 0)
            {
                Info($"T'es mort !");
                GameManager.Instance.IsOnGameOver = true;
            }
        }

        public void TakeDamage(int amount)
        {
            _characterStat.TakeDamage(amount);
            Info($"Le joueur prend {amount} degats, il lui reste {_characterStat.CurrentHealth} PV");
        }
    }
}


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

        private void Update()
        {
            if (_characterStat.CurrentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        
        public void TakeDamage(int amount)
        {
            Info($"Il prend {amount} degats, il lui reste {_characterStat.CurrentHealth} PV");
            _characterStat.TakeDamage(amount);
        }
    }
}


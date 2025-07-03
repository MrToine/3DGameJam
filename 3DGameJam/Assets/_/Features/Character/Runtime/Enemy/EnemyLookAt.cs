using UnityEngine;

namespace Character.Runtime
{
    public class EnemyLookAt : MonoBehaviour
    {
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

        void Update()
        {
            LookAtPlayer();
        }

        private Transform _player;
    }
}

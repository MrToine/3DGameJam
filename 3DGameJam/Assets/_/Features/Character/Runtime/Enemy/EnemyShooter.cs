using System.Collections;
using Character.SO;
using Core.Runtime;
using ProjectileSystem.Runtime;
using UnityEngine;

namespace Character.Runtime
{
    public class EnemyShooter : BaseMonobehaviour
    {

        #region Publics

        public void Init(EnemyType type, GameObject bulletPrefab, StatDatas stat)
        {
            _enemyType = type;
            _bulletPrefab = bulletPrefab;
            _stat = stat;
        }

        #endregion


        #region Unity API

        private void Awake()
        {
            _bulletSpawner = GetComponent<BulletSpawner>();
            _player = GameObject.FindWithTag("Player").GetComponent<Player.Player>();
        }

        private void Update()
        {
            if(GameManager.Instance.IsOnGameOver || GameManager.Instance.IsOnPause) return;
            if (!_isShooting)
            {
                StartCoroutine(Waiting());
            }
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        private IEnumerator Waiting()
        {
            _isShooting = true;
            yield return new WaitForSeconds(_waitTimeToShoot);
            if (_enemyType == EnemyType.Elite)
            {
                _bulletSpawner.Spawn(_bulletPrefab, transform.position, transform.forward, _stat);
            }
            else
            {
                Info("HIT");
                _player.TakeDamage(_stat.AttackPower);
            }
            _isShooting = false;
        }

        #endregion


        #region Privates and Protected

        [Header( "Timer" )]
        [SerializeField] private float _waitTimeToShoot;

        private bool _isShooting = false;
        private BulletSpawner _bulletSpawner;
        private int _test = 0;
        private EnemyType _enemyType;
        private GameObject _bulletPrefab;
        private StatDatas _stat;
        private Player.Player _player;

        #endregion
    }
}


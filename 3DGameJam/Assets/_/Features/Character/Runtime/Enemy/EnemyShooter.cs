using System.Collections;
using Core.Runtime;
using ProjectileSystem.Runtime;
using UnityEngine;

namespace Character.Runtime
{
    public class EnemyShooter : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void Awake()
        {
            _bulletSpawner = GetComponent<BulletSpawner>();
        }

        private void Update()
        {
            if (!_isShooting)
            {
                StartCoroutine(Waiting());
                Info($"Tir numéro {_test++}");
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
            _bulletSpawner.OnShot();
            _isShooting = false;
        }

        #endregion


        #region Privates and Protected

        [Header( "Timer" )]
        [SerializeField] private float _waitTimeToShoot;

        private bool _isShooting = false;
        private BulletSpawner _bulletSpawner;
        private int _test = 0;

        #endregion
    }
}


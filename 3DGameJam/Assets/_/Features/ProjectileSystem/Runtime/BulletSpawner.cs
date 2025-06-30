using UnityEngine;

namespace ProjectileSystem.Runtime
{
    public class BulletSpawner : MonoBehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void Awake()
        {
            _poolSystem = GetComponentInParent<PoolSystem.Runtime.PoolSystem>();
        }

        #endregion


        #region Main Methods

        public void OnShot()
        {
            var bullet = _poolSystem.GetFirstAvailableProjectile();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        private PoolSystem.Runtime.PoolSystem _poolSystem;

        #endregion
    }
}


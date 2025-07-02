using Character.SO;
using Core.Runtime;
using UnityEngine;

namespace ProjectileSystem.Runtime
{
    public class BulletSpawner : BaseMonobehaviour
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
        
        public void Spawn(GameObject bulletPrefab, Vector3 transformPosition, Vector3 transformForward, StatDatas stat)
        {
            var bullet = _poolSystem.GetFirstAvailableProjectile();
            bullet.transform.position = transformPosition;
            bullet.transform.rotation = Quaternion.LookRotation(transformForward);
            bullet.SetActive(true);
            
            var bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript)
            {
                bulletScript.SetStats(stat);
            }
            else
            {
                Warning("Le projectile n'a pas de script Bullet");
            }
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


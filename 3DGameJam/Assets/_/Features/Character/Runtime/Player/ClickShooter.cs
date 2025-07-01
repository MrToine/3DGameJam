using System.Collections;
using System.Diagnostics;
using Character.SO;
using Core.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Runtime.Player
{
    public class ClickShooter : BaseMonobehaviour
    {

        #region Publics

        public enum WeaponType
        {
            Gun,
            Shotgun,
        }

        #endregion


        #region Unity API

        private void Awake()
        {
            _currentWeapon = WeaponType.Gun;
            _characterStat = GetComponent<CharacterStat>();
            _camera = Camera.main;
        }

        #endregion


        #region Main Methods

        public void OnShot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                CheckWeapon();
                Shot();
            }
        }

        private void CheckWeapon()
        {
           switch(_currentWeapon){

               case  WeaponType.Gun:
                   _radius = _gunStats.m_radius;
                   _damage = _gunStats.m_damage;
                   break;
               case WeaponType.Shotgun:
               
                   _radius = _shotGunStats.m_radius;
                   _damage = _shotGunStats.m_damage;
                   break;  
               
           }
        }
        #endregion


        #region Utils

        /* Fonctions privées utiles */
        private void Shot()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (_radius > 0f)
            {
                RaycastHit[] hits = Physics.SphereCastAll(ray, _radius, 200,_enemyLayer);
                // Shotgun : SphereCast
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
                    {
                        damageable.TakeDamage(_damage);
                    }
                }
            }
            else
            {
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, _enemyLayer))
                {
                    if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                    {
                        Info("Je tire sur ta race de merde l'ennemie");
                        damageable.TakeDamage(_damage);
                    }
                }
            }
        }

        #endregion


        #region Privates and Protected

        [Header("References")]
        [SerializeField] private LayerMask _enemyLayer;

        [SerializeField] private WeaponStat _shotGunStats;
        [SerializeField] private WeaponStat _gunStats;

        private Camera _camera;
        private CharacterStat _characterStat;

        public WeaponType _currentWeapon;
        private float _radius;
        private int _damage;

        #endregion


    }
}


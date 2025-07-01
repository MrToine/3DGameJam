using System.Collections;
using System.Diagnostics;
using Character.SO;
using Core.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

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

        public void Weapon1(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _currentWeapon = WeaponType.Gun;
            }
        }
        public void Weapon2(InputAction.CallbackContext context)
        {
            if (context.performed && m_shotgunUnlocked)
            {
                _currentWeapon = WeaponType.Shotgun;
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
                   _shotGunFallOffDistance = _shotGunStats.m_shotGunFallOffDistance;
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
                    Debug.Log("Distance: " + hit.distance);
                    
                    if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                    {
                        damageable.TakeDamage(hit.distance > _shotGunFallOffDistance ? _damage/2 : _damage);
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

        public bool m_shotgunUnlocked;
        public WeaponType _currentWeapon;
        private float _radius;
        private int _damage;
        private float _shotGunFallOffDistance;

        #endregion

        
    }
}


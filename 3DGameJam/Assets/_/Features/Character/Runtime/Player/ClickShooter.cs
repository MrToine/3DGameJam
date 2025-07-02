using System;
using AudioSystem.Runtime;
using Character.SO;
using Core.Runtime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace Character.Runtime.Player
{
    public class ClickShooter : BaseMonobehaviour
    {

        #region Publics
        
        public UnityEvent<int> OnShotEvent;

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
            _shotCount = 15;
        }

        #endregion


        #region Main Methods

        public void Reload(InputAction.CallbackContext context)
        {
            Info("On tente de recharger");
            if (context.performed && GameManager.Instance.BattleAreaEnd == false && GameManager.Instance.IsOnPause == false)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.SfxLibrary[0]);
                _shotCount = 15;
                OnShotEvent?.Invoke(_shotCount);
            }
        }

        public void OnShot(InputAction.CallbackContext context)
        {
            if (context.performed && _shotCount >= 0 && GameManager.Instance.BattleAreaEnd == false && GameManager.Instance.IsOnPause == false && _shotCount > 0)
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
            AudioManager.Instance.PlaySFX(AudioManager.Instance.SfxLibrary[1]);
            if (_shotCount >= 1)
            {
                _shotCount--;
                OnShotEvent?.Invoke(_shotCount);
            }
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector3 origin = _camera.transform.position;

            if (_currentWeapon == WeaponType.Shotgun)
            {
                RaycastHit[] hits = Physics.SphereCastAll(ray, _radius, 200f, _enemyLayers);

                foreach (RaycastHit hit in hits)
                {
                    Vector3 targetPoint = hit.point;
                    float distance = Vector3.Distance(origin, targetPoint);

                    // Obstacle check
                    if (Physics.Raycast(origin, (targetPoint - origin).normalized, out RaycastHit blockHit, distance, _obstacleLayers))
                    {
                        // Un mur bloque la vue vers l'ennemi → on ignore
                        continue;
                    }

                    if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                    {
                        int finalDamage = hit.distance > _shotGunFallOffDistance ? _damage / 2 : _damage;
                        damageable.TakeDamage(finalDamage);
                    }
                }
            }
            else if (_currentWeapon == WeaponType.Gun)
            {
                // On cast sur tout ce qui peut bloquer (ennemis + obstacles)
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, _enemyLayers | _obstacleLayers))
                {
                    // On vérifie si c’est un ennemi
                    if (((1 << hit.collider.gameObject.layer) & _enemyLayers) != 0)
                    {
                        if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                        {
                            damageable.TakeDamage(_damage);
                        }
                        else
                        {
                            Warning("La cible n'a pas de composant IDamageable");
                        }
                    }
                    else
                    {
                        Debug.Log("Tir bloqué par : " + hit.collider.name);
                    }
                }
                else
                {
                    Warning("Cible hors de portée");
                }
            }
        }


        #endregion


        #region Privates and Protected

        [Header("References")]
        //[SerializeField] private LayerMask _enemyLayer;
        [SerializeField] private LayerMask _enemyLayers;
        [SerializeField] private LayerMask _obstacleLayers;

        [SerializeField] private WeaponStat _shotGunStats;
        [SerializeField] private WeaponStat _gunStats;

        private Camera _camera;
        private CharacterStat _characterStat;

        public bool m_shotgunUnlocked;
        public WeaponType _currentWeapon;
        private float _radius;
        private int _damage;
        private float _shotGunFallOffDistance;
        private int _shotCount;
        private bool _canReload = false;

        #endregion


    }
}


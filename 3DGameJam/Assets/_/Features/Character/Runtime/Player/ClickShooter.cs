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

        public MeshRenderer m_gunRenderer;
        public MeshRenderer m_shotGunRenderer;
        public UnityEvent<int> OnShotEvent;
        public UnityEvent OnEmptyMagazieEvent;
        public UnityEvent OnReloadingEvent;
        

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
            CheckWeapon();
            _characterStat = GetComponent<CharacterStat>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_isReloading)
            {
                _counter += Time.deltaTime;
                if (_counter >= _reloadTime)
                {
                    _isReloading = false;
                    _shotCount = CurrentWeaponStat.m_magazine;
                    OnShotEvent?.Invoke(_shotCount);
                    _counter = 0;
                }
            }
        }

        #endregion


        #region Main Methods

        public void Reload(InputAction.CallbackContext context)
        {
            if (context.performed && GameManager.Instance.BattleAreaEnd == false && GameManager.Instance.IsOnPause == false)
            {
                _isReloading = true;
                OnReloadingEvent.Invoke();
                AudioManager.Instance.PlaySFX(AudioManager.Instance.SfxLibrary[0]);
              
            }
        }

        public void OnShot(InputAction.CallbackContext context)
        {
            if (context.performed && _shotCount >= 0 && GameManager.Instance.BattleAreaEnd == false && GameManager.Instance.IsOnPause == false && _shotCount > 0)
            {
                if (_isReloading) return;
                Shot();
            }
        }

        public void Weapon1(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _currentWeapon = WeaponType.Gun;
                CheckWeapon();
                
            }
        }
        public void Weapon2(InputAction.CallbackContext context)
        {
            if (context.performed && m_shotgunUnlocked)
            {
                
                _currentWeapon = WeaponType.Shotgun;
                CheckWeapon();
                
            }
        }
        private void CheckWeapon()
        {
           _radius = CurrentWeaponStat.m_radius;
           _damage = CurrentWeaponStat.m_damage;
           _shotCount = CurrentWeaponStat.m_magazine;
           _reloadTime = CurrentWeaponStat.m_reloadTime;
           _shotGunFallOffDistance = CurrentWeaponStat.m_shotGunFallOffDistance;
           _renderer.material = CurrentWeaponStat.m_material;
           _meshFilter.mesh = CurrentWeaponStat.m_mesh;

        }
        #endregion


        #region Utils

        /* Fonctions privées utiles */
        private void Shot()
        {
            AudioManager.Instance.PlaySFXByName("retro-gun");
            if (_shotCount >= 1)
            {
                _shotCount--;
                OnShotEvent?.Invoke(_shotCount);
            }

            if (_shotCount <= 0)
            {
                Debug.Log("You have to reload");
                OnEmptyMagazieEvent?.Invoke();
                return;
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

        [SerializeField] private MeshRenderer _renderer;
        [SerializeField] private MeshFilter _meshFilter;
        private Camera _camera;
        private CharacterStat _characterStat;

        public bool m_shotgunUnlocked;
        public WeaponType _currentWeapon;
        private float _radius;
        private int _damage;
        private float _shotGunFallOffDistance;
        private int _shotCount;
        private float _reloadTime;
        private bool _isReloading;
        private float _counter;
        private bool _canReload = false;

        
        private WeaponStat CurrentWeaponStat =>
            _currentWeapon == WeaponType.Gun ? _gunStats : _shotGunStats;


        #endregion


    }
}


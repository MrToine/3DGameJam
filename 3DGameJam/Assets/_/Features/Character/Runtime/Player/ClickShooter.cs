using Character.SO;
using Core.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Runtime.Player
{
    public class ClickShooter : BaseMonobehaviour
    {
        #region Unity API

        private void Awake()
        {
            _characterStat = GetComponent<CharacterStat>();
            _camera = Camera.main;
        }

        #endregion


        #region Main Methods

        public void OnShot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Shot();
            }
        }
        
        #endregion


        #region Utils

        /* Fonctions privées utiles */
        private void Shot()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _enemyLayer))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out var damageable))
                {
                    Info("Je tire sur ta race de merde l'ennemie");
                    damageable.TakeDamage(_characterStat.AttackPower);
                }
            }
        }

        #endregion


        #region Privates and Protected

        [Header("References")]
        [SerializeField] private LayerMask _enemyLayer;

        private Camera _camera;
        private CharacterStat _characterStat;

        #endregion


    }
}


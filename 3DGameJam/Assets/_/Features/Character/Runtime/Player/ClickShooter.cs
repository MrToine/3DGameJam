using Core.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character.Runtime.Player
{
    public class ClickShooter : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods

        public void OnShot(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Info("Je tire sur ta race de merde l'ennemie");
            }
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        [Header("References")]
        [SerializeField] private LayerMask _enemyMask;

        #endregion
    }
}


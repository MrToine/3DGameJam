using System;
using Core.Runtime;
using UnityEngine;

namespace MenuSystem.Runtime
{
    public class GameOver : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        void Update()
        {
            Info("Game over ?");
            if (!GameManager.Instance.IsOnGameOver)
            {
                Info("Pas game over");
                enabled = false;
            }
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        // Variables privées

        #endregion
    }
}


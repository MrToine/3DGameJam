using Core.Runtime;
using TMPro;
using UnityEngine;

namespace Character.Runtime.Player
{
    public class MagazineUI : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void Awake()
        {
            _magazineText = GetComponentInChildren<TMP_Text>();
            _clickShooter = GetComponentInParent<ClickShooter>();
            _clickShooter.OnShotEvent.AddListener(UpdateMagazine);
        }

        void UpdateMagazine(int count)
        {
            _magazineText.text = count.ToString();
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        private TMP_Text _magazineText;
        private ClickShooter _clickShooter;

        #endregion
    }
}


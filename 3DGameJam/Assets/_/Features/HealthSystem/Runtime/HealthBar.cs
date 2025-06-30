using System;
using Core.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace HealthSystem.Runtime
{
    public class HealthBar : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void Awake()
        {
            if (_health == null)
            {
                Error("HealthBar : Health is null");
            }
        }

        private void OnEnable()
        {
            if (_health != null)
            {
                _health.OnHealthChange += UpdateBar;
            }
        }

        void OnDisable()
        {
            if (_health != null)
            {
                _health.OnHealthChange -= UpdateBar;
            }
        }

        private void Update()
        {
            UpdateBar();
        }

        #endregion


        #region Main Methods

        // 

        #endregion


        #region Utils

        private void UpdateBar()
        {
            if (_health == null || _fillImage == null)
            {
                return;
            }
            float ration = _health.CurrentHealth / _health.MaxHealth;
            _fillImage.fillAmount = ration;
        }

        #endregion


        #region Privates and Protected

        [SerializeField] private Image _fillImage;
        [SerializeField] private Health _health;

        #endregion
    }
}


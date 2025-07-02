using System;
using AudioSystem.Runtime;
using Core.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace MenuSystem.Runtime
{
    public class SettingsMenu : BaseMonobehaviour, IMenuModule
    {

        #region Publics

        public string GetMenuName() => "Settings";
        public GameObject GetMenuPanel() => _settingsPanel;

        #endregion


        #region Unity API

        private void Start()
        {
            AudioManager.Instance.BinderVolume(_masterVolumeSlider, AudioManager.AudioChannel.Master);
            AudioManager.Instance.BinderVolume(_musicVolumeSlider, AudioManager.AudioChannel.Music);
            AudioManager.Instance.BinderVolume(_ambianceVolumeSlider, AudioManager.AudioChannel.Ambiance);
            AudioManager.Instance.BinderVolume(_sfxVolumeSlider, AudioManager.AudioChannel.SFX);
        
            if (MenuManager.Instance != null)
            {
                MenuManager.Instance.RegisterMenu(this);
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

        [SerializeField] private GameObject _settingsPanel;

        [Header("SLiders Audio Settings")]
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _ambianceVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;

        #endregion

    }
}


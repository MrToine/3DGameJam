using System.Collections;
using Core.Runtime;
using UnityEngine.UI;
using UnityEngine;

namespace Character.Runtime.Player
{
    public class ScreenDamageFlash : BaseMonobehaviour
    {
        #region Unity API

        private void Awake()
        {
            _image = GetComponent<Image>();
        }
        
        #endregion

        #region Main Methods

        public void Flash()
        {
            StopAllCoroutines();
            StartCoroutine(FlashRoutine());
        }

        #endregion


        #region Utils

        private IEnumerator FlashRoutine()
        {
            _image.color = new Color(1, 0, 0, _flashIntensity);
            yield return new WaitForSeconds(0.1f);
            _image.color = new Color(1, 0, 0, 0f);
        }

        #endregion


        #region Privates and Protected
        
        [SerializeField] private float _flashDuration = 0.1f;
        [SerializeField] private float _flashIntensity = 0.5f;
        
        private Image _image;

        #endregion
    }
}


using System.Collections;
using Core.Runtime;
using TMPro;
using UnityEngine;

namespace Character.Runtime.Player
{
    public class MagazineUI : BaseMonobehaviour
    {
        #region Unity API

        private void Awake()
        {
            _magazineText = GetComponentInChildren<TMP_Text>();
            _clickShooter.OnShotEvent.AddListener(UpdateMagazine);
            _clickShooter.OnEmptyMagazieEvent.AddListener(() => StartBlinking(_reloadMagazineText, _reloadFlashColor));
            _clickShooter.OnReloadingEvent.AddListener(() => StartBlinking(_reloadingMagazineText, _reloadingFlashColor));
        }

        void UpdateMagazine(int count)
        {
            StopBlinking();
            _magazineText.text = count.ToString();
            _magazineText.color = _defaultColor;
        }

        #endregion

        #region Main Methods

        private void StartBlinking(string message, Color flashColor)
        {
            StopBlinking();
            _magazineText.text = message;
            _magazineText.color = flashColor;
            _blinkRoutine = StartCoroutine(FadeText(flashColor));
        }

        private void StopBlinking()
        {
            if (_blinkRoutine != null)
            {
                StopCoroutine(_blinkRoutine);
                _blinkRoutine = null;
            }

            _magazineText.color = _defaultColor;
        }

        private IEnumerator FadeText(Color baseColor)
        {
            float duration = 1f;
            float alphaMin = 0.3f;
            float alphaMax = 1f;

            while (true)
            {
                float t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime / duration;
                    float alpha = Mathf.Lerp(alphaMax, alphaMin, t);
                    _magazineText.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
                    yield return null;
                }

                t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime / duration;
                    float alpha = Mathf.Lerp(alphaMin, alphaMax, t);
                    _magazineText.color = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
                    yield return null;
                }
            }
        }

        #endregion

        #region Privates

        private TMP_Text _magazineText;
        private Coroutine _blinkRoutine;

        [Header( "Reference Player")]
        [SerializeField] private ClickShooter _clickShooter;
        [Header( "Color & text")]
        [SerializeField] private string _reloadMagazineText;
        [SerializeField] private string _reloadingMagazineText;
        [SerializeField] private Color _reloadFlashColor = Color.red;
        [SerializeField] private Color _reloadingFlashColor = Color.green;
        [SerializeField] private Color _defaultColor = Color.white;

        #endregion
    }
}

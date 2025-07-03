using System;
using Character.Runtime.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Character.Runtime
{
    public class Shotgun : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textComponent;
        
        private void Update()
        {
            _counter += Time.deltaTime;
            if (_counter > _despawnTime)
            {
                _textComponent.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var cs = other.GetComponent<ClickShooter>();
                cs.m_shotgunUnlocked = true;
                cs._currentWeapon = ClickShooter.WeaponType.Shotgun;
               DisplayText();
            }
        }

        
        private void DisplayText()
        {
            _textComponent.gameObject.SetActive(true);
        }
        
        private float _counter;
        [SerializeField] private float _despawnTime;
    }
}

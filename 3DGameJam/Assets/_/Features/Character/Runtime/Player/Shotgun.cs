using System;
using Character.Runtime.Player;
using UnityEngine;

namespace Character.Runtime
{
    public class Shotgun : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var cs = other.GetComponent<ClickShooter>();
                cs.m_shotgunUnlocked = true;
                cs._currentWeapon = ClickShooter.WeaponType.Shotgun;
            }
        }
    }
}

using System;
using UnityEngine;

namespace Character.Runtime
{
    public class BackLineKiller : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.gameObject.layer);
            if (other.gameObject.layer == LayerMask.NameToLayer("EnemyNear"))
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}

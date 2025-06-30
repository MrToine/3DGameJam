using Character.SO;
using Core.Runtime;
using UnityEngine;
using PoolSystem.Runtime;
using Unity.Cinemachine;

namespace ProjectileSystem.Runtime
{
    public class Bullet : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void OnEnable()
        {
            _camera = Camera.main;
            Vector3 direction = (_camera.transform.position - transform.position).normalized;
            Rigidbody.AddForce(direction * _forwardPower + Vector3.up * _upPower, ForceMode.Impulse);
        }

        private void Update()
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {  
                gameObject.SetActive(false);
                Rigidbody.linearVelocity = Vector3.zero;
                Rigidbody.angularVelocity = Vector3.zero;
                _timer = _lifetime;
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
        
        [Header( "Scriptable Object (enemy for attack stat)" )]
        [SerializeField] private SOStat _stat;
        [Header( "Bullet Parameters" )]
        [SerializeField] private float _lifetime;
        [Header("Trajectory")]
        [SerializeField] private float _forwardPower;
        [SerializeField] private float _upPower;
        [SerializeField] private float _speed;

        private Camera _camera;
        private float _timer;

        #endregion

    }
}


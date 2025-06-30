using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Runtime
{
    public class PlayerCameraInfluence : MonoBehaviour
    {
        private void Update()
        {
            //HandleRotation();
        }

        
        private void HandleRotation()
        {
            Vector3 camForward = _camera.transform.forward; 
            Vector3 camRight = _camera.transform.right;
            
            camForward.y = 0f; 
            camRight.y = 0f;

            camForward.Normalize(); 
            camRight.Normalize();
            
            //_movementInput = (camForward * input.look.y + camRight * input.look.x).normalized;

            
            Debug.Log(_movementInput);
                if (_movementInput != Vector3.zero)
                {
                    Quaternion targetRot = Quaternion.LookRotation(_movementInput); 
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 10f);
                }
        }
        
        private Vector3 _movementInput;
        //[SerializeField] private StarterAssetsInputs input;
        [SerializeField] private InputActionAsset _actions;
        [SerializeField] private CinemachineCamera _camera;
    }
}

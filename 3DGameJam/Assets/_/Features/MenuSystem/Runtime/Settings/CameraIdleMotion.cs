using UnityEngine;

namespace MenuSystem.Runtime
{
    public class CameraIdleMotion : MonoBehaviour
    {
        public float angleX = 1.5f;
        public float angleY = 1.0f;
        public float frequency = 0.5f;

        private Quaternion startRotation;

        void Start()
        {
            startRotation = transform.localRotation;
        }

        void Update()
        {
            float rotX = Mathf.Sin(Time.time * frequency) * angleX;
            float rotY = Mathf.Cos(Time.time * frequency) * angleY;

            transform.localRotation = startRotation * Quaternion.Euler(rotX, rotY, 0f);
        }
    }
}

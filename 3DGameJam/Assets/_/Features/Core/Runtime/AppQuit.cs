using UnityEngine;

namespace Core.Runtime
{
    public class AppQuit : MonoBehaviour
    {
        public void OnApplicationQuit()
        {
            Application.Quit();
        }
    }
}

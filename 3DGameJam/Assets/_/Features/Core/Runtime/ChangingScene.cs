using UnityEngine;

namespace Core.Runtime
{
    public class ChangingScene : MonoBehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        void OnTriggerEnter(Collider other)
        {
            SceneLoader.Instance.FakeLoading(_nextScene);
        }

        #endregion


        #region Main Methods

        public void SetNextScene(string nextScene)
        {
            SceneLoader.Instance.FakeLoading(nextScene);
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        // Variables privées
        [SerializeField] private string _nextScene;

        #endregion
    }
}


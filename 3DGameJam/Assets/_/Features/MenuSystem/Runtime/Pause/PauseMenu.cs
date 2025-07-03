using System;
using Core.Runtime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace MenuSystem.Runtime
{
        public class PauseMenu : BaseMonobehaviour
        {
                #region Publics

                public bool IsOpen;
            
                #endregion


                #region Unity API
                
                void Update()
                {
                        if (IsOpen)
                        {
                                SlideIn();
                        }
                        else
                        {
                                SlideOut();
                        }
                        
                        if (Input.GetKeyDown(KeyCode.Escape))
                        {
                                TogglePause();
                        }
                }

                void TogglePause()
                {
                        IsOpen = !IsOpen;
                        Time.timeScale = IsOpen ? 0 : 1;
                        GameManager.Instance.IsOnPause = IsOpen;
                        
                        _panel.gameObject.SetActive(IsOpen);
                }


                #endregion
                

                #region Main Methods

                public void Resume()
                {
                        TogglePause();
                }

                public void Restart()
                {
                        GameManager.Instance.IsOnGameOver = false;
                        GameManager.Instance.ReloadScene();
                }
                
                public void MainMenu()
                {
                        GameManager.Instance.IsOnGameOver = false;
                        SceneLoader.Instance.LoadScene("Menu");
                }
                
                public void Quit()
                {
                        Application.Quit();
                }

                public void SetMainPanelVisible(bool visible)
                {
                        _defaultFocusPanel.SetActive(visible);
                }
            
                #endregion

            
                #region Utils

                private void SlideIn()
                {
                        Info("On slide IN");
                        //_panel.position = new Vector2(200, _panel.position.y);
                        //_panel.transform.DOMove(new Vector3(0, -944, 0), 0.3f).SetEase(Ease.Linear).SetUpdate(true);
                        //_inputActionAsset.actionMaps[0].Disable();
                        //_inputActionAsset.actionMaps[1].Enable();
                        _panel.gameObject.SetActive(true);
                }
                
                private void SlideOut()
                {
                        Info("On slide OUT");
                        //_panel.position = new Vector2(-200, _panel.position.y);
                        //_panel.transform.DOMove(new Vector3(0,805,0), 0.3f).SetEase(Ease.Linear);
                        //_inputActionAsset.actionMaps[0].Enable();
                        //_inputActionAsset.actionMaps[1].Disable();
                        _panel.gameObject.SetActive(false);
                }
            
                #endregion
            
            
                #region Privates and Protected

                [Header("Referencement")]
                [SerializeField] private RectTransform _panel;
                [SerializeField] private GameObject _defaultFocusPanel;
                [Header("Input Settings")]
                [SerializeField] private InputActionAsset _inputActionAsset;

                #endregion
        }
}

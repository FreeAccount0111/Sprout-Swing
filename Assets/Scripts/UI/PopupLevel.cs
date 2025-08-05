using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class PopupLevel : BasePopup
    {
        [SerializeField] private List<LevelBtn> levelBtns = new List<LevelBtn>();
        [SerializeField] private Button btnHome;

        private void Awake()
        {
            for(int i = 0; i < levelBtns.Count; i++)
            {
                int index = i;
                levelBtns[i].AddListener(() =>
                {
                    if (!UserData.GetLevelLock(index))
                    {
                            //UserData.SetCurrentLevel(index);
                            GameManager.Instance.LoadGame(index);
                            
                            HideImmediately(true);
                            PopupCtrl.Instance.GetPopupByType<PopupGameplay>().ShowImmediately(false);
                        
                    }
                });
            }

            btnHome.onClick.AddListener(() =>
            {
                HideImmediately(true);
                PopupCtrl.Instance.GetPopupByType<PopupHome>().ShowImmediately(false);
            });
        }

        public override void ShowImmediately(bool showImmediately, Action actionComplete = null)
        {
            base.ShowImmediately(showImmediately, actionComplete);
            UpdateLockLevel();
        }

        public void UpdateLockLevel()
        {

        }
    }
}

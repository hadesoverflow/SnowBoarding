using System.Collections.Generic;
using _GAME.Scripts;
using DenkKits.GameServices.SaveData;
using DenkKits.UIManager.Scripts.UIPopup;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DenkKits.GameTemplate.Scripts.Popup
{
    public class GameModePopup : UIPopup
    {
        [SerializeField] private List<Button> levelListButtons;
        [SerializeField] private List<GameObject> lockedIcon;

        protected override void OnInit()
        {
            base.OnInit();
            for (int i = 0; i < levelListButtons.Count; i++)
            {
                var i1 = i;
                levelListButtons[i].onClick.AddListener(() => LoadGame(i1));
            }
        }

        protected override void OnShowing()
        {
            base.OnShowing();

            var currentLevel = SaveDataHandler.Instance.saveData.level;
            for (int i = 0; i < levelListButtons.Count; i++)
            {
                var i1 = i;
                levelListButtons[i].onClick.AddListener(() =>  LoadGame(i1));
                if (currentLevel >= i)
                {
                    levelListButtons[i].interactable = true;
                    lockedIcon[i].SetActive(false);
                }
                else
                {
                    levelListButtons[i].interactable = false;
                    lockedIcon[i].SetActive(true);
                }
            }
        }


        public void LoadGame(int mode)
        {
            SaveDataHandler.Instance.saveData.currentLevelIndex = mode;
            Hide();
            UIManager.Scripts.Base.UIManager.Instance.ShowTransition(() =>
            {
                SceneManager.LoadScene(GameConstants.SceneGame);
            });
        }
    }
}
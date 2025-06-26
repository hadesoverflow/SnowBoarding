using _GAME.Scripts.Popup;
using DenkKits.GameServices.Audio.Scripts;
using DenkKits.GameServices.Manager;
using DenkKits.GameServices.SaveData;
using DenkKits.UIManager.Scripts.Base;
using DenkKits.UIManager.Scripts.UIPopup;
using DenkKits.UIManager.Scripts.UIView;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GAME.Scripts.Views
{
    public class MainView : UIView
    {
        [SerializeField] private TextMeshProUGUI highScoreText;
        [SerializeField] private TextMeshProUGUI timeHighScore;

        protected override void OnInit()
        {
            base.OnInit();
            SoArchitectureManager.Instance.OnMoneyChangedEvent.AddListener(() =>
            {
                highScoreText.text = SaveDataHandler.Instance.UserHighScore.ToString();
            });
        }

        protected override void OnShowing()
        {
            base.OnShowing();
            highScoreText.text = SaveDataHandler.Instance.UserHighScore.ToString();
            if (SaveDataHandler.Instance.UserHighScoreTime == -1)
            {
                timeHighScore.text = "N/A";
            }
            else
            {
                timeHighScore.text = SaveDataHandler.Instance.UserHighScoreTime.ToString();
            }
        }

        public void QUIT()
        {
            Application.Quit();
        }
        #region MAIN UI BUTTON CALLBACK

        public void OnClickPlayGame()
        {
            // Hide();
            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.GameModePopup);
        }

        public void OnClickOpenSetting()
        {
            var param = new SettingPopupParam
            {
                showGroupBtn = false
            };
            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.SettingPopup, param);
        }


        public void OnClickLeaderBoard()
        {
            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.LeaderBoardPopup);
        }
        public void OnClickHowToPlay()
        {
            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.TutorialPopup);
        }
        public void OnClickShop()
        {
            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.ShopPopup);
        }
        #endregion
    }
}
using System;
using DenkKits.GameServices.Audio.Scripts;
using DenkKits.GameServices.Manager;
using DenkKits.UIManager.Scripts.Base;
using DenkKits.UIManager.Scripts.UIPopup;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _GAME.Scripts.Popup
{
    public class SettingPopupParam
    {
        public bool showGroupBtn;
    }

    public class SettingPopup : UIPopup
    {
        [Serializable]
        struct SettingLanguageItemData
        {
            public int languageId;
            public Sprite icon;
            public string name;
        }

        [SerializeField] private Slider musicSetting;
        [SerializeField] private Slider audiSetting;
        [SerializeField] private GameObject groupBtn;

        public void OnClickExit()
        {
            Hide();
            UIManager.Instance.ShowTransition(() => { SceneManager.LoadScene(GameConstants.SceneMain); });
        }

        public void OnClickPlayAgain()
        {
            Hide();
            UIManager.Instance.ShowTransition(() => { SceneManager.LoadScene(GameConstants.SceneGame); });
        }

        protected override void OnShowing()
        {
            base.OnShowing();

            SoArchitectureManager.Instance.PauseGame.Raise();
            musicSetting.SetValueWithoutNotify(AudioManager.Instance!.musicVolume);
            audiSetting.SetValueWithoutNotify(AudioManager.Instance!.audioVolume);

            var param = (SettingPopupParam)Parameter;
            if (param != null)
            {
                groupBtn.SetActive(param.showGroupBtn);
            }
        }

        protected override void OnHiding()
        {
            base.OnHiding();

            SoArchitectureManager.Instance.ResumeGame.Raise();
            AudioManager.Instance.SaveAudioSetting();
        }

        public void OnChangeMusic(float value)
        {
            AudioManager.Instance.SetMusicVolume(value);
        }

        public void OnChangeAudio(float value)
        {
            AudioManager.Instance.SetAudioVolume(value);
        }
    }
}
using System.Collections;
using _GAME.Scripts.Popup;
using AssetKits.ParticleImage;
using DenkKits.GameServices.Audio.Scripts;
using DenkKits.UIManager.Scripts.Base;
using DenkKits.UIManager.Scripts.UIPopup;
using DenkKits.UIManager.Scripts.UIView;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _GAME.Scripts.Views
{
    public class GameView : UIView
    {
        [SerializeField] private GameObject scoreUI;
        [SerializeField] private TextMeshProUGUI currentQuestion;
        [SerializeField] private TextMeshProUGUI timer;
        [SerializeField] private TextMeshProUGUI currentAnwserChoosen;
        [SerializeField] private TextMeshProUGUI currentScore;
        [SerializeField] private TextMeshProUGUI Combostreak;
        [SerializeField] private TextMeshProUGUI getScoreEf;
        [SerializeField] private RectTransform moneyPanel;
        [SerializeField] private ParticleImage moneyParticleImage;
        
        int userScore = 0;
        public override void Awake()
        {
            base.Awake();
            moneyParticleImage.onFirstParticleFinished.AddListener(OnMoneyFirstParticleFinished);
        }

        public void ShowScoreUI( bool show = true )
        {
            scoreUI.SetActive(show);
        }

        public void UpdateQuestionIndex(int index)
        {
            currentQuestion.text = $"{index}";
        }

        public void UpdateTimer(float time)
        {
            timer.text = time.ToString("0.00");
        }

        public void UpdateAnswerChoosen(string answer)
        {
            currentAnwserChoosen.text = answer;
        }

        public void ShowCombostreak(int streak)
        {
            if (streak == 1)
            {
                return;
            }

            Combostreak.gameObject.SetActive(true); // Bật lên
            Combostreak.text = $"Combo Streak: {streak}";

            Combostreak.transform.localScale = Vector3.one * 0.5f;
            Combostreak.color = new Color(Combostreak.color.r, Combostreak.color.g, Combostreak.color.b, 1f);

            // Hiệu ứng phóng to rồi thu nhỏ nhẹ
            Combostreak.transform
                .DOScale(1f, 0.3f)
                .SetEase(Ease.OutBack);

            // Làm mờ sau 1 giây
            Combostreak.DOFade(0, 1f)
                .SetDelay(1f)
                .OnComplete(() => { Combostreak.gameObject.SetActive(false); });
        }

        public void UpdateScore(int score)
        {
            userScore = score;
            currentScore.text = $"{score}";
        }


        public void OpenSetting()
        {
            var param = new SettingPopupParam
            {
                showGroupBtn = true
            };
            AudioManager.Instance.PlaySfx(AudioName.UI_Click);
            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.SettingPopup, param);
        }

        public void UpdateScoreWithParticleImage(int score)
        {
            moneyParticleImage.Play();
            userScore = score;
        }
        private void OnMoneyFirstParticleFinished()
        {
            currentScore.text = $"{userScore}";
        }
        
      
    }
}
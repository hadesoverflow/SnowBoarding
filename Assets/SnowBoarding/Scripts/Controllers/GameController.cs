using System.Collections.Generic;
using _GAME.Scripts.Views;
using DenkKits.GameServices.Audio.Scripts;
using DenkKits.GameServices.Manager;
using DenkKits.GameServices.SaveData;
using DenkKits.UIManager.Scripts.Base;
using DenkKits.UIManager.Scripts.UIPopup;
using DenkKits.UIManager.Scripts.UIView;
using Imba.Utils;
using SnowBoarding.Scripts.GamePlay;
using SnowBoarding.Scripts.GamePlay.Map;
using UnityEngine;

namespace _GAME.Scripts.Controllers
{
    public class GameController : ManualSingletonMono<GameController>
    {
        private bool _isGamePaused = false;
        private int _userScore = 0;
        private int mapIndex = 0;
        private GameView _gameView;
        [SerializeField] float boostSpeed = 30f;
        [SerializeField] float baseSpeed = 20f;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private BG gameBG;
        [SerializeField] private List<GameObject> mapList;
        public LevelController currentLevel;

        public override void Awake()
        {
            base.Awake();
            SoArchitectureManager.Instance.PauseGame.AddListener(PauseGame);
            SoArchitectureManager.Instance.ResumeGame.AddListener(ResumeGame);
        }

        void Start()
        {
            mapIndex = SaveDataHandler.Instance.saveData.currentLevelIndex;
            if (mapIndex > mapList.Count - 1)
            {
                var map = Instantiate(mapList[^1]);
                currentLevel = map.GetComponent<LevelController>();
            }
            else
            {
                var map = Instantiate(mapList[mapIndex]);
                currentLevel = map.GetComponent<LevelController>();
            }

            gameBG.SetBG(currentLevel.bgType);
            _gameView = UIManager.Instance.ViewManager.GetViewByName<GameView>(UIViewName.GameView);
            playerController.transform.position = currentLevel.startPoint.position;
            _gameView.UpdateScore(0);
            UIManager.Instance.ViewManager.ShowView(UIViewName.GameView);
            UIManager.Instance.HideTransition(() => { });
        }

        void Update()
        {
            if (_isGamePaused) return;

            HandlePlayerInput();

            // if (Input.GetKeyDown(KeyCode.L))
            // {
            //     ShowEndGame();
            // }
        }

        private void HandlePlayerInput()
        {
            if (playerController == null) return;

            // Boost
            bool isBoosting = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
            foreach (var VARIABLE in     currentLevel.surfaceEffector2D)
            {
                VARIABLE.speed = isBoosting ? boostSpeed : baseSpeed;

            }

            // Rotation
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                playerController.RotateLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                playerController.RotateRight();
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerController.TryJump();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIManager.Instance.PopupManager.ShowPopup(UIPopupName.SettingPopup);
            }
        }


        private void PauseGame()
        {
            _isGamePaused = true;
            playerController.rb2d.isKinematic = true;
            playerController.rb2d.velocity = Vector2.zero;
            foreach (var VARIABLE in     currentLevel.surfaceEffector2D)
            {
                VARIABLE.speed = 0;

            }
        }

        private void ResumeGame()
        {
            _isGamePaused = false;
            playerController.rb2d.isKinematic = false;
            foreach (var VARIABLE in     currentLevel.surfaceEffector2D)
            {
                VARIABLE.speed = baseSpeed;

            }
        }

        public void PlayerHitFinishLine()
        {
            AudioManager.Instance.PlaySfx(AudioName.Gameplay_PlayerScore);
            if (mapIndex == SaveDataHandler.Instance.saveData.level)
            {
                SaveDataHandler.Instance.saveData.level++;
            }

            isWin = true;
            SaveDataHandler.Instance.RequestSave();
            ShowEndGame();
        }

        bool isWin = false;

        public void PlayerHitCoin()
        {
            _userScore++;
            AudioManager.Instance.PlaySfx(AudioName.Gameplay_PlayerScore);
            _gameView.UpdateScoreWithParticleImage(_userScore);
        }

        public void PlayerHitObstacle()
        {
            AudioManager.Instance.PlaySfx(AudioName.Gameplay_WeaponExplosion);
            AudioManager.Instance.PlaySfx(AudioName.Gameplay_PlayerLose);
            ShowEndGame();
        }

        /// <summary>
        /// GAME ENDED
        /// </summary>
        public void ShowEndGame()
        {
            PauseGame();

            EndGamePopupParam param = new EndGamePopupParam
            {
                score = _userScore,
                isNewHighScore = false,
                win = isWin
            };

            SaveDataHandler.Instance.UserHighScore += _userScore;
            SaveDataHandler.Instance.RequestSave();

            UIManager.Instance.PopupManager.ShowPopup(UIPopupName.EndGamePopup, param);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            SoArchitectureManager.Instance.PauseGame.RemoveListener(PauseGame);
            SoArchitectureManager.Instance.ResumeGame.RemoveListener(ResumeGame);
        }
    }
}
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace DenkKits.GameServices.SaveData
{
    [Serializable]
    public class SaveData
    {
        [Title("Strings")] public string profileName = "";
        public int playerRegion;
        public string userID = "";
        public string lastTimeEarnedDailyReward = DateTime.Now.ToString();
        public string lastDateMonthlyChallenge = DateTime.Now.ToString();
        public string rankingCountdownTime;

        [Title("Tutorials")]
        public bool tapPlayTutorial;

        [Title("Numbers")]
        public int level = 0;
        public int currentLevelIndex = 0;
        public int moneyAmount;
        public int currentDayDailyReward = 1;
        public int userHighScore = 0;
        public int gameModeChoose = 0;
        
        public int userHighScoreTime = -1;

        [Title("Booleans")]
        public bool isShowRateUs;

        [Title("Lists")] 
        public List<int> dailyRewardClaimedList = new();
        
        [Title("Shop System")]
        public List<string> ownedSkins = new()
        {
            "default",
        };
        public string equippedSkinID = "default";


        
    }
}
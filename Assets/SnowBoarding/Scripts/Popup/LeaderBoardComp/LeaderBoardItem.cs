using TMPro;
using UnityEngine;

namespace DenkKits.Sample.Scripts.Popup.LeaderBoardComp
{
    public class LeaderBoardItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textRank;
        [SerializeField] private TextMeshProUGUI textUser;
        [SerializeField] private TextMeshProUGUI textScore;

        public void InitItem(int rank, string userName, int score)
        {
            switch (rank)
            {
                case 1:
                    textRank.color = Color.red;
                    break;
                case 2:
                    textRank.color = Color.yellow;
                    break;
                case 3:
                    textRank.color = Color.green;
                    break;
                default:
                    textRank.color = Color.black;
                    break;
            }

            textRank.text = rank.ToString();
            textUser.text = userName;
            textScore.text = score.ToString();
        }
    }
}
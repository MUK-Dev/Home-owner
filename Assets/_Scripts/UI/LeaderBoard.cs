using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class LeaderBoard : MonoBehaviour
{
    public static LeaderBoard Instance { get; private set; }
    [SerializeField] private List<TextMeshProUGUI> names;
    [SerializeField] private List<TextMeshProUGUI> scores;
    private string publicLeaderboardKey = "e8f9bbcd277045ca5155eafc1794c85c7880c1f429072990f1869d84ffc74452";
    private string[] badWords = {
   
};

    private void Awake()
    {
        Instance = this;
        GetLeaderboardKey();
    }

    public void GetLeaderboardKey()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            for (int i = 0; i < loopLength; i++)
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString();
            }
        }));
    }



    public void SetLeaderboardEntry()
    {
        string username = PlayerPrefs.GetString("PlayerName");
        float score = PlayerPrefs.GetFloat("HighScore", 0f); // Default value of 0.0f if high score is not set

        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, (int)score, ((msg) =>
        {
            if (System.Array.IndexOf(badWords, username) != -1) return;
            GetLeaderboardKey();
        }));
    }

}

using System;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
//using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    //[SerializeField] private Button backToMenuButton;
    //[SerializeField] private Button continueWithAdButton;
    [SerializeField] private TextMeshProUGUI highScoreText;

    public static GameOverUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        HomeGameManager.Instance.OnStateChange += HomeGameManager_OnStateChange;
        Hide();
    }

    private void HomeGameManager_OnStateChange(object sender, EventArgs e)
    {
        var gameState = HomeGameManager.Instance.GetCurrentState();
        if (gameState == HomeGameManager.State.GameOver)
        {
            if (PlayerPrefs.HasKey("RewardedScore"))
            {
                PlayerPrefs.DeleteKey("RewardScore");
            }

            // Check if the new score is higher than the current high score
            float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
            if (ScoreUI.instance.currentScore > highScore )
            {
                highScore = ScoreUI.instance.currentScore;

                // Save the new high score to PlayerPrefs
                PlayerPrefs.SetFloat("HighScore", highScore);
                PlayerPrefs.Save();

                UpdateHighScoreUI(highScore);

                Debug.Log("New High Score: " + highScore);
            }
            Show();
        }
    }

    public void ReturnToMenu()
    {
        SoundEffectsManager.Instance.PlayBeepSound();
        Loader.Load(Loader.Scene.MainMenu);
        BannerAd.Instance.ShowBannerAd();
    }

    public void UpdateHighScoreUI(float highScore)
    {
        highScoreText.text = highScore.ToString();
        LeaderBoard.Instance.SetLeaderboardEntry();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}

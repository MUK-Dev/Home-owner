using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public static ScoreUI instance;
    [SerializeField] private GameObject gameoverUi;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI gameoverCurrentScore;
    [HideInInspector] public float currentScore;

    private bool hasUpdatedWithRewardedScore = false; // Flag to track if rewarded score has been used

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ClickObject.Instance.OnEnemyKill += ClickObject_OnEnemyKill;

        // Load the high score from PlayerPrefs
        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        UpdateHighScoreUI(highScore);

        // Check if "RewardScore" exists in PlayerPrefs and it hasn't been used yet
        if (PlayerPrefs.HasKey("RewardScore") && !hasUpdatedWithRewardedScore)
        {
            float rewardedScore = PlayerPrefs.GetFloat("RewardScore");
            currentScore += rewardedScore;
            score.text = currentScore.ToString();

            // Update current score text
            gameoverCurrentScore.text = currentScore.ToString();

            // Delete the "RewardScore" key
            PlayerPrefs.DeleteKey("RewardScore");

            // Update flag to indicate rewarded score has been used
            hasUpdatedWithRewardedScore = true;
        }
    }

    private void ClickObject_OnEnemyKill(object sender, ClickObject.OnEnemyKillEventArgs e)
    {
        if (!gameoverUi.activeSelf) // Check if game over UI is not active
        {
            float previousScore = currentScore;
            currentScore += e.killScore;

            if (hasUpdatedWithRewardedScore)
            {
                // Add to the rewarded score instead of replacing it
                currentScore += PlayerPrefs.GetFloat("RewardScore");
                hasUpdatedWithRewardedScore = false; // Reset the flag
            }

            score.text = currentScore.ToString();

            // Check if the new score is higher than the current high score
            float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
            if (currentScore > highScore && gameoverUi.activeSelf)
            {
                highScore = currentScore;

                // Save the new high score to PlayerPrefs
                PlayerPrefs.SetFloat("HighScore", highScore);
                PlayerPrefs.Save();

                UpdateHighScoreUI(highScore);
            }

            // Update current score text
            gameoverCurrentScore.text = currentScore.ToString();
        }
    }

    private void UpdateHighScoreUI(float highScore)
    {
        GameOverUI.Instance.UpdateHighScoreUI(highScore);
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private GameObject gameoverUi;
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI gameoverCurrentScore;

    private float currentScore;

    private void Start()
    {
        ClickObject.Instance.OnEnemyKill += ClickObject_OnEnemyKill;

        // Load the high score from PlayerPrefs
        float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        UpdateHighScoreUI(highScore);
    }

    private void Update()
    {
        // Check if the game is over
        if (gameoverUi.activeSelf)
        {
            score.gameObject.SetActive(false);
            gameoverCurrentScore.text = currentScore.ToString();
        }
    }

    private void ClickObject_OnEnemyKill(object sender, ClickObject.OnEnemyKillEventArgs e)
    {
        if (!gameoverUi.activeSelf) // Check if game over UI is not active
        {
            float previousScore = currentScore;
            currentScore += e.killScore;
            score.text = currentScore.ToString();

            // Check if the new score is higher than the current high score
            float highScore = PlayerPrefs.GetFloat("HighScore", 0f);
            if (currentScore > highScore)
            {
                highScore = currentScore;

                // Save the new high score to PlayerPrefs
                PlayerPrefs.SetFloat("HighScore", highScore);
                PlayerPrefs.Save();

                UpdateHighScoreUI(highScore);
            }
        }
    }

    private void UpdateHighScoreUI(float highScore)
    {
        GameOverUI.Instance.UpdateHighScoreUI(highScore);
    }
}

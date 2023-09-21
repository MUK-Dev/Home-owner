using System;
using TMPro;
using UnityEngine;
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
            Show();
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateHighScoreUI(float highScore)
    {
        highScoreText.text = highScore.ToString();
        LeaderBoard.Instance.SetLeaderboardEntry();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}

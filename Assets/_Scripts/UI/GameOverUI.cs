using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button backToMenuButton;
    [SerializeField] private Button continueWithAdButton;

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


    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}

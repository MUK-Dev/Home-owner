using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;

    void Start()
    {
        HomeGameManager.Instance.OnStateChange += HomeGameManager_OnStateChange;
    }

    private void HomeGameManager_OnStateChange(object sender, EventArgs e)
    {
        //* If game state is count down timer then show timer
        var gameState = HomeGameManager.Instance.GetCurrentState();
        if (gameState == HomeGameManager.State.CountdownToStart)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        //* Update timer text
        countdownText.text = Mathf.Ceil(HomeGameManager.Instance.GetCountdownToStartTimer()).ToString();
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);


}

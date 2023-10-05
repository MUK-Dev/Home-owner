using UnityEngine;
using System;

public class Tip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HomeGameManager.Instance.OnStateChange += HomeGameManager_OnStateChange;
        gameObject.SetActive(true);
    }

    private void HomeGameManager_OnStateChange(object sender, EventArgs e)
    {
        if (HomeGameManager.Instance.GetCurrentState() != HomeGameManager.State.CountdownToStart)
        {
            gameObject.SetActive(false);
        }
    }
}

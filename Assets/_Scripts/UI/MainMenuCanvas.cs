using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public void StartButtonClick()
    {
        Debug.Log("Clicked");
        Loader.Load(Loader.Scene.GameScene);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}

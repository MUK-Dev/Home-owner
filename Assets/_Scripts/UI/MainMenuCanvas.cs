using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public void StartButtonClick()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}

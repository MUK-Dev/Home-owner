using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject leaderBoard;
    public void StartButtonClick()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    public void Leaderboard()
    {
        leaderBoard.SetActive(true);
        menu.SetActive(false);
    }

    public void BackButton()
    {

        leaderBoard.SetActive(false);
        menu.SetActive(true);
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}

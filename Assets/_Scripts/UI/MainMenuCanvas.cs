using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject leaderBoard;
    public void StartButtonClick()
    {
        SoundEffectsManager.Instance.PlayBeepSound();
        Loader.Load(Loader.Scene.GameScene);
    }

    public void Leaderboard()
    {
        SoundEffectsManager.Instance.PlayBeepSound();
        leaderBoard.SetActive(true);
        menu.SetActive(false);
    }

    public void BackButton()
    {
        SoundEffectsManager.Instance.PlayBeepSound();
        leaderBoard.SetActive(false);
        menu.SetActive(true);
    }

    public void ExitButtonClick()
    {
        SoundEffectsManager.Instance.PlayBeepSound();
        Application.Quit();
    }
}

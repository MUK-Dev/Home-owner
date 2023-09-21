using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UsernameManager : MonoBehaviour
{
    [SerializeField] private GameObject welcomeBackObject;
    [SerializeField] private GameObject enterNameObject;
    [SerializeField] private GameObject errorPopup;
    [SerializeField] private TextMeshProUGUI welcomeText;
    [SerializeField] private TMP_InputField nameInputField;
    public string playerName;

    private void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName");

        if (!string.IsNullOrEmpty(playerName))
        {
            welcomeBackObject.SetActive(true);
            welcomeText.text = playerName + "!";
            Invoke("LoadSceneAfterDelay", 3f);
        }
        else
        {
            enterNameObject.SetActive(true);
        }
    }

    public void OnSubmitButtonClicked()
    {
        playerName = nameInputField.text;
        if (!string.IsNullOrEmpty(playerName) && playerName.Length <= 10)
        {
            PlayerPrefs.SetString("PlayerName", playerName);
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            errorPopup.SetActive(true);
        }
    }

    private void LoadSceneAfterDelay()
    {
        SceneManager.LoadScene("MainMenu");
    }

}

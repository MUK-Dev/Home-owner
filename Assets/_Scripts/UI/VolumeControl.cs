using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private Image volumeImage;
    [SerializeField] private Button volumeButton;
    [SerializeField] private Sprite volumeOnImage;
    [SerializeField] private Sprite volumeOffImage;

    private void Start()
    {
        UpdateImage();
        volumeButton.onClick.AddListener(() =>
        {
            BackgroundMusic.Instance.ToggleMusic();
            UpdateImage();
        });
    }

    private void UpdateImage()
    {
        bool isPlaying = BackgroundMusic.Instance.GetMusicStatus();
        if (isPlaying)
            volumeImage.sprite = volumeOnImage;
        else
            volumeImage.sprite = volumeOffImage;
    }

}

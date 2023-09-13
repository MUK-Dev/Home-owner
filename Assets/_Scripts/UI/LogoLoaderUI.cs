using TMPro;
using UnityEngine;

public class LogoLoaderUI : MonoBehaviour
{

    private TextMeshProUGUI _dots;

    private float _dotsUpdateTimer = 0f;
    private float _dotsUpdateTimerMax = .5f;


    void Start()
    {
        _dots = gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _dotsUpdateTimer += Time.deltaTime;
        if (_dotsUpdateTimer >= _dotsUpdateTimerMax)
        {
            _dotsUpdateTimer = 0f;
            UpdateDots();
        }
    }

    private void UpdateDots()
    {
        if (_dots.text == ".") _dots.text = "..";
        else if (_dots.text == "..") _dots.text = "...";
        else if (_dots.text == "...") _dots.text = ".";
    }
}

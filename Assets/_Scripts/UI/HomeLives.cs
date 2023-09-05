using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomeLives : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private Animator heartAnimator;

    private const string LIFE_REDUCE = "LifeReduce";

    private void Awake()
    {
        Show();
    }

    private void Start()
    {
        Home.Instance.OnLifeUpdate += Home_OnLifeUpdate;
    }

    private void Home_OnLifeUpdate(object sender, Home.OnLifeUpdateEventArgs e)
    {
        livesText.text = e.life.ToString();
        heartAnimator.SetTrigger(LIFE_REDUCE);
    }

    private void Show() => gameObject.SetActive(true);
    private void Hide() => gameObject.SetActive(false);
}

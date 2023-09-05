using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI score;

    private void Start()
    {
        ClickObject.Instance.OnEnemyKill += ClickObject_OnEnemyKill;
    }

    private void ClickObject_OnEnemyKill(object sender, ClickObject.OnEnemyKillEventArgs e)
    {
        float previousScore = float.Parse(score.text);
        score.text = (previousScore + e.killScore).ToString();
    }
}

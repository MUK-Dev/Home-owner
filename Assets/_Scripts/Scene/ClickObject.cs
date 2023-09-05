using UnityEngine;
using System;


public class ClickObject : MonoBehaviour
{
    public static ClickObject Instance { get; private set; }

    public event EventHandler<OnEnemyKillEventArgs> OnEnemyKill;

    public class OnEnemyKillEventArgs : EventArgs
    {
        public float killScore;
    }


    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Enemy touchedEnemy = hit.collider.gameObject.GetComponent<Enemy>();
                    float killScore = touchedEnemy.KillEnemy();
                    if (killScore != -1)
                    {
                        OnEnemyKill?.Invoke(this, new OnEnemyKillEventArgs { killScore = killScore });
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    Enemy touchedEnemy = hit.collider.gameObject.GetComponent<Enemy>();
                    float killScore = touchedEnemy.KillEnemy();
                    if (killScore != -1)
                    {
                        OnEnemyKill?.Invoke(this, new OnEnemyKillEventArgs { killScore = killScore });
                    }
                }
            }
        }
    }

}

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

    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Application.isMobilePlatform)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            Vector2 raycastBoxSize = new Vector2(.5f, .5f);

            RaycastHit2D hit = Physics2D.BoxCast(ray.origin, raycastBoxSize, 0, ray.direction);

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
        if (Input.GetMouseButtonDown(0) && Application.isEditor)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            Vector2 raycastBoxSize = new Vector2(.5f, .5f);

            RaycastHit2D hit = Physics2D.BoxCast(ray.origin, raycastBoxSize, 0, ray.direction);
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

using UnityEngine;


public class ClickObject : MonoBehaviour
{
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
                    touchedEnemy.KillEnemy();
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
                    touchedEnemy.KillEnemy();
                }
            }
        }
    }

}

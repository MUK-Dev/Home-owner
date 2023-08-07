using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed = 0.0025f;
    private Rigidbody2D rb;

    private void Start()
    {
        //? Get the rigid body of the component
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //* Get the Direction to the target
        //* Rotate towards the target
        if (target) RotateTowardsTarget();
    }

    private void RotateTowardsTarget()
    {
        //* Rotate the enemy towards the Home
        Vector2 targetDirection = target.position - transform.position;

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void FixedUpdate()
    {
        //* Move Forwards
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Home"))
        {
            //? Check if the collided object was the home
            bool wasHouseDestroyed = Home.Instance.TryDestroyHouse();
            if (!wasHouseDestroyed)
            {
                KillEnemy();
            }
            else
            {
                target = null;
            }
        }
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }

}

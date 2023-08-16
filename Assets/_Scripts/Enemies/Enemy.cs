using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int life = 1;
    [SerializeField] private bool _shouldSlerp = true;

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
        //? Angle of the home from enemy
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        //? Rotation of enemy towards home
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Quaternion enemyRotation;
        //? Check if this enemy circulates and rotates towards home
        if (_shouldSlerp) enemyRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotateSpeed);
        //? or goes straight for it?
        else enemyRotation = targetRotation;

        transform.localRotation = enemyRotation;
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
            //? Damage the house with the enemys health
            bool wasHouseDestroyed = Home.Instance.TryDestroyHouse(life);
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
        //? If enemys life is not 0 then simply reduce its health
        if (life > 1) life -= 1;
        //? If it is zero then destroy it
        else Destroy(gameObject);
    }

}

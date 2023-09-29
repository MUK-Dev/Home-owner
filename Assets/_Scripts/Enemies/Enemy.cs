using UnityEngine;

public class Enemy : MonoBehaviour
{

    private const string HIT = "Hit";

    [SerializeField] private Transform target;
    [SerializeField] private ParticleSystem blood;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private int life = 1;
    [SerializeField] private bool _shouldSlerp = true;
    [SerializeField] private float killingScore;

    private Rigidbody2D _rb;
    private Animator _animator;
    private float _tapDelay = 0f;


    private void Start()
    {
        //? Get the rigid body & animator component
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
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
        _rb.velocity = transform.up * speed;
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
                Destroy(gameObject);
            }
            else
            {
                target = null;
            }
        }
    }

    public float KillEnemy()
    {
        if (_tapDelay <= 0)
        {
            //? if there is no tap delay then attack enemy and add delay
            if (Application.isMobilePlatform) _tapDelay = .1f;

            //? If enemys life is not 0 then reduce its health
            if (life > 1)
            {
                _animator.SetTrigger(HIT);
                life -= 1;
                return -1;
            }
            //? If it is zero then destroy it
            else
            {
                SoundEffectsManager.Instance.PlayBeepSound();
                Destroy(gameObject);
                Instantiate(blood, transform.position, Quaternion.identity);
                return killingScore;
            }
        }
        else
        {
            //? if there is tap delay then reduce tap delay
            _tapDelay -= Time.deltaTime;
            return -1;
        }
    }

}

using UnityEngine;

public class Home : MonoBehaviour
{
    public static Home Instance { get; private set; }

    [SerializeField] private int _houseLife = 3;

    private void Awake()
    {
        Instance = this;
    }

    public bool TryDestroyHouse(int damage)
    {
        if (_houseLife > 1)
        {
            _houseLife -= damage;
            if (_houseLife <= 0)
            {
                Destroy(gameObject);
                return true;
            }
            return false;
        }
        else
        {
            Destroy(gameObject);
            return true;
        }
    }
}

using UnityEngine;

public class Home : MonoBehaviour
{
    public static Home Instance { get; private set; }

    [SerializeField] private int _houseLife = 3;

    private void Awake()
    {
        Instance = this;
    }

    public bool TryDestroyHouse()
    {
        if (_houseLife > 1)
        {
            _houseLife -= 1;
            return false;
        }
        else
        {
            Destroy(gameObject);
            return true;
        }
    }
}

using UnityEngine;
using System;

public class Home : MonoBehaviour
{
    public static Home Instance { get; private set; }

    public event EventHandler<OnLifeUpdateEventArgs> OnLifeUpdate;

    public class OnLifeUpdateEventArgs : EventArgs
    {
        public int life;
    }

    [SerializeField] private int _houseLife = 3;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnLifeUpdate?.Invoke(this, new OnLifeUpdateEventArgs { life = _houseLife });
    }

    public bool TryDestroyHouse(int damage)
    {
        //? Reduce house life
        _houseLife -= damage;
        if (_houseLife <= 0)
        {
            //? If life is less than 1 then destroy the house
            Destroy(gameObject);
            OnLifeUpdate?.Invoke(this, new OnLifeUpdateEventArgs { life = _houseLife });
            return true;
        }
        OnLifeUpdate?.Invoke(this, new OnLifeUpdateEventArgs { life = _houseLife });
        return false;
    }
}

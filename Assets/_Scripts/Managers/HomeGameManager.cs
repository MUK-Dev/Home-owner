using UnityEngine;

public class HomeGameManager : MonoBehaviour
{
    public static HomeGameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}

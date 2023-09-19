using System.Threading;
using UnityEngine;

public class LoaderCallback : MonoBehaviour
{
    private bool isFirstUpdate = true;

    private float _loaderTimer = 2f;

    private void Update()
    {
        if (_loaderTimer >= 0) _loaderTimer -= Time.deltaTime;

        if (isFirstUpdate && _loaderTimer <= 0)
        {
            isFirstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}

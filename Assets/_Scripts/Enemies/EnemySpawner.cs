using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyTypes;
    [SerializeField] private float _activationTimer;
    [SerializeField] private bool _vertical;

    private float _spawnTimer = 0f;
    private float _spawnTimerMax = 5f;
    private int _unlockedEnemyRange = 0;
    private float _elapsedTime = 0f;
    private bool _activated = false;

    private void Start()
    {
        //? If the activation timer is 0 then spawn the first enemy
        if (_activationTimer <= 0f) Spawn();
    }

    private void Update()
    {
        //? Check if spawner is activated
        if (!_activated) CheckActivation();
        //? if yes then spawn enemies with time
        else SpawnEnemyWithTime();
    }

    private void CheckActivation()
    {
        //* Check if gameplay time has reached activation timer

        if (_elapsedTime < _activationTimer)
        {
            _elapsedTime += Time.deltaTime;
        }
        //* If yes then activate the spawn point
        else
        {
            _activated = true;
        }
    }

    private void SpawnEnemyWithTime()
    {
        //* Increment the spawn timer and when it reaches max time spawn the enemy
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnTimerMax)
        {
            _spawnTimer = 0f;
            Spawn();
        }
    }

    private void Spawn()
    {
        //* Instantiation of enemy from the enemy type
        //? Reseting the spawn point scale
        gameObject.transform.localScale = Vector3.one;

        Vector3 gameObjectPosition = gameObject.transform.localPosition;

        //? Generating a random spawn point between range
        Vector3 spawnPostion;
        //? Checking if this is a vertical spawn point
        if (_vertical) spawnPostion = new Vector3(gameObjectPosition.x, gameObjectPosition.y + Random.Range(0, 5), gameObjectPosition.z);
        //? or horizontal
        else spawnPostion = new Vector3(gameObjectPosition.x + Random.Range(0, 5), gameObjectPosition.y, gameObjectPosition.z);

        //? Selecting a random enemy from the unlocked enemies
        Instantiate(_enemyTypes[Random.Range(0, _unlockedEnemyRange)], spawnPostion, transform.rotation);
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemyTypes;
    [SerializeField] private float _activationTimer;
    [SerializeField] private bool _vertical;
    [SerializeField] private float _increaeDifficultyAfter;
    [SerializeField] private float _increaseSpawnRateAfter;

    private float _spawnTimer = 0f;
    private float _spawnTimerMax = 5f;
    private int _unlockedEnemyRange = 0;
    private float _elapsedTime = 0f;
    private float _increaseDifficultyTimer;
    private float _increaseSpawnRateTimer;
    private bool _activated = false;

    private HomeGameManager.State _currentState;

    private void Start()
    {
        //? If the activation timer is 0 then spawn the first enemy
        HomeGameManager.Instance.OnStateChange += HomeGameManager_OnStateChange;
    }

    private void HomeGameManager_OnStateChange(object sender, EventArgs e)
    {
        _currentState = HomeGameManager.Instance.GetCurrentState();
    }

    private void Update()
    {
        if (_currentState == HomeGameManager.State.GamePlaying)
        {
            //* Increase timers here
            _elapsedTime += Time.deltaTime;
            _increaseDifficultyTimer += Time.deltaTime;
            _increaseSpawnRateTimer += Time.deltaTime;
        }
        //? Check if spawner is activated
        if (!_activated) CheckActivation();
        //? if yes then spawn enemies with time
        else if (_currentState == HomeGameManager.State.GamePlaying) SpawnEnemyWithTime();
    }

    private void CheckActivation()
    {
        if (HomeGameManager.Instance.GetCurrentState() != HomeGameManager.State.CountdownToStart)
        {
            //? If game is not during the countdown state
            //* Check if gameplay time has reached activation timer
            //* and activation timer is not equal to 0f
            if (_elapsedTime > _activationTimer || _activationTimer <= 0f)
            {
                //* If yes then activate the spawn point
                _activated = true;
            }

        }
    }

    private void SpawnEnemyWithTime()
    {
        //? Increment the spawn timer and when it reaches max time spawn the enemy
        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnTimerMax)
        {
            _spawnTimer = 0f;
            Spawn();
        }

        //? Increase the types of enemy that can be spawned
        if (_increaseDifficultyTimer >= _increaeDifficultyAfter)
        {
            if (_unlockedEnemyRange != _enemyTypes.Count) _unlockedEnemyRange++;
            _increaeDifficultyAfter = 0f;
        }

        //? Increase spawn rate of enemies
        if (_increaseSpawnRateTimer >= _increaseSpawnRateAfter)
        {
            if (_spawnTimerMax > 1) _spawnTimerMax -= 0.2f;
            _increaseSpawnRateTimer = 0f;
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
        if (_vertical) spawnPostion = new Vector3(gameObjectPosition.x, gameObjectPosition.y + UnityEngine.Random.Range(0, 5), gameObjectPosition.z);
        //? or horizontal
        else spawnPostion = new Vector3(gameObjectPosition.x + UnityEngine.Random.Range(0, 5), gameObjectPosition.y, gameObjectPosition.z);

        //? Selecting a random enemy from the unlocked enemies
        Instantiate(_enemyTypes[UnityEngine.Random.Range(0, _unlockedEnemyRange)], spawnPostion, transform.rotation);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //serialize fields for obstacle and food prefabs
    [SerializeField]
    private GameObject[] _obstaclePrefabs;
    [SerializeField]
    private GameObject[] _foodPrefabs;

    //seting other variables
    private GameManager _gameManager;
    private float _spawnPositionX = 30.0f;
    private float _obstacleSpawnPeriod = 1.5f;
    //_foodSpawnPeriod = _obstacleSpawnPeriod * integer number; delay = _obstacleSpawnPeriod / 2 + _foodSpawnPeriod
    private float _foodSpawnPeriod = 4.5f;
    private float _foodSpawnDelay = 5.25f;
    private float _foodSpawnRange = 5.0f;

    void Start()
    {
        //initilize gameManager and repeatedly invoke SpawnRandomObstacle and SpawnRandomFood methods
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating("SpawnRandomObstacle", 0, _obstacleSpawnPeriod);
        InvokeRepeating("SpawnRandomFood", _foodSpawnDelay, _foodSpawnPeriod);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomObstacle()
    {
        //if game is started and not over
        if (_gameManager.isGameStarted && !_gameManager.gameOver)
        {
            //generate random index in array
            int index = Random.Range(0, _obstaclePrefabs.Length);
            //set the spawn position and instantiate new object
            Vector3 spawnPosition = new Vector3(_spawnPositionX, _obstaclePrefabs[index].transform.position.y, _obstaclePrefabs[index].transform.position.z);
            Instantiate(_obstaclePrefabs[index], spawnPosition, _obstaclePrefabs[index].transform.rotation);
        }
    }

    void SpawnRandomFood()
    {
        //if game is started and not over
        if (_gameManager.isGameStarted && !_gameManager.gameOver)
        {
            //generate random index in array
            int index = Random.Range(0, _foodPrefabs.Length);
            //set the spawn position and instantiate new object
            Vector3 spawnPosition = new Vector3(_spawnPositionX, Random.Range(_foodSpawnRange, -_foodSpawnRange), _foodPrefabs[index].transform.position.z);
            Instantiate(_foodPrefabs[index], spawnPosition, _foodPrefabs[index].transform.rotation);
        }
    }
}

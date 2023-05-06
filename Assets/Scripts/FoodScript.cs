using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{
    //serialized field for particle sysctem
    [SerializeField]
    private GameObject _particleSystem;

    //initializing/declaration other variables
    private GameManager _gameManager;
    private float _destroyBounds = -15.0f;
    private float _speed = 10.0f;
    private float _rotationSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //!!! hardcoded: animation needed. Rotation of child gameObject
        transform.GetChild(0).Rotate(Vector3.forward * Time.deltaTime * _rotationSpeed);

        //if game is not over
        if (!_gameManager.gameOver)
        {
            //move food left
            transform.Translate(Vector3.left * Time.deltaTime * _speed);

            //if food is passed destroy bounds
            if (transform.position.x < _destroyBounds)
            {
                //destroy food
                Destroy(gameObject);
            }
        }
    }

    //when collides with player
    private void OnTriggerEnter(Collider other)
    {
        float timeOut = 2.0f;
        //create particle
        GameObject particleSystem = Instantiate(_particleSystem, transform.position, transform.rotation);
        //destroy particle after 2 seconds
        Destroy(particleSystem, timeOut);
        //destroy food
        Destroy(gameObject);
    }
}

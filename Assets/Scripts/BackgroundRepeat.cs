using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    //initilizing/declaration variables
    private float _speed = 10.0f;
    private float _repeatPosition;
    private Vector3 _startPosition;
    private BoxCollider _backgroundCollider;
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _backgroundCollider = GetComponent<BoxCollider>();
        //repeat position = background width / 2
        _repeatPosition = _backgroundCollider.size.x / 2;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if game is not over
        if (!_gameManager.gameOver)
        {
            //move background left
            transform.Translate(Vector3.left * Time.deltaTime * _speed);

            //if half of the width is passed
            if (transform.position.x <= _startPosition.x - _repeatPosition)
            {
                //restart position
                transform.position = _startPosition;
            }
        }
    }
}

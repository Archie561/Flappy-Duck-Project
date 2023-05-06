using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour
{
    //initilizing/declaration variables
    private GameManager _gameManager;
    private AudioSource _audioSource;
    private float _speed = 10.0f;
    private float _playerPassBounds;
    private float _destroyBounds = -15.0f;
    private bool _isClipPlayed;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        //setting playerPassBounds to x player position - 1
        _playerPassBounds = GameObject.Find("Player").transform.position.x - 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //if game is not over
        if (!_gameManager.gameOver)
        {
            //move obstacle left with speed _speed
            transform.Translate(Vector3.left * Time.deltaTime * _speed);

            //if obstacle pass _playerPassBounds and passing sound is not played yet
            if (transform.position.x < _playerPassBounds && !_isClipPlayed)
            {
                //add score
                _gameManager.AddScore();
                //play passing sound
                _audioSource.Play();
                //set _isClipPlayed to true
                _isClipPlayed = true;
            }
            //if obstacle x possition less then _destroyBounds
            if (transform.position.x < _destroyBounds)
            {
                //destroy obstacle
                Destroy(gameObject);
            }
        }
    }
}

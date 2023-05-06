using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //get gameManager and player components
    private GameManager _gameManager;
    private Rigidbody _playerRigidbody;
    private Animator _playerAnimator;
    private AudioSource _playerAudioSource;

    //get audioclips for special events
    [SerializeField]
    private AudioClip _jumpSound;
    [SerializeField]
    private AudioClip _deathSound;
    [SerializeField]
    private AudioClip _pickupSound;

    //player controller variables
    private float _jumpForce = 60f;
    private float _gravityMultiplier = 2.5f;
    private float _topMapBorder = 6.0f;
    private float _bottomMapBorder = -10.0f;

    //variables for pre-game lerp
    private Vector3 _startPositionIdle;
    private Vector3 _endPositionIdle;
    private float _endPositionHeight = 1.0f;
    private float _idleMoveDuration = 1.0f;
    private float _elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        //initializing all needed variables
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimator = GetComponent<Animator>();
        _playerAudioSource = GetComponent<AudioSource>();
        Physics.gravity *= _gravityMultiplier;

        _startPositionIdle = transform.position;
        _endPositionIdle = new Vector3(transform.position.x, transform.position.y + _endPositionHeight, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        //if game is not started, play lerp animation
        if (!_gameManager.isGameStarted)
        {
            _playerAnimator.SetTrigger("Jump");

            _elapsedTime += Time.deltaTime;
            float percantageComplete = _elapsedTime / _idleMoveDuration;
            transform.position = Vector3.Lerp(_startPositionIdle, _endPositionIdle, percantageComplete);

            //if player in the destenition point, reverse destenition point
            if (_elapsedTime > _idleMoveDuration)
            {
                _elapsedTime = 0;
                _startPositionIdle = transform.position;
                _endPositionHeight = -_endPositionHeight;
                _endPositionIdle = new Vector3(transform.position.x, transform.position.y + _endPositionHeight, transform.position.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !_gameManager.gameOver)
        {
            //when the player first hit space bar
            if (!_gameManager.isGameStarted)
            {
                //unfreeze movement and set isGameStarted to true
                _playerRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                _gameManager.StartGame();
            }

            //set the velocity of the player to 0 before jump
            _playerRigidbody.velocity = Vector3.zero;
            //add impulse to simulate jump
            _playerRigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            //play animation and jump sound
            _playerAnimator.SetTrigger("Jump");
            _playerAudioSource.PlayOneShot(_jumpSound);
        }

        //if player y position is higher than topMapBorder
        if (transform.position.y >= _topMapBorder)
        {
            //set player y position to topMapBorder
            transform.position = new Vector3(transform.position.x, _topMapBorder, transform.position.z);
        }
        //if player y position is less than bottomMapBorder
        if (transform.position.y <= _bottomMapBorder)
        {
            //if game is not alredy over
            if (!_gameManager.gameOver)
            {
                //set isGameOver property to true and play death sound
                _playerAudioSource.PlayOneShot(_deathSound);
                _gameManager.GameOver();
            }

            //destroy player after 2 seconds
            float delay = 2.0f;
            Destroy(gameObject, delay);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if player collides with an obstacle
        if (collision.gameObject.CompareTag("NormalObstacle"))
        {
            //if game is not alredy over
            if (!_gameManager.gameOver)
            {
                //set isGameOver property to true and play death sound
                _playerAudioSource.PlayOneShot(_deathSound);
                _gameManager.GameOver();
            }
            //add force to the player to knock it out of the frame
            _playerRigidbody.AddForce((Vector3.left + Vector3.up) * _jumpForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if player collides with any fruit, play pickup sound and add additional score
        if (other.gameObject.CompareTag("Avocado"))
        {
            _playerAudioSource.PlayOneShot(_pickupSound);
            _gameManager.AddScore();
        }
        else if (other.gameObject.CompareTag("Banana"))
        {
            _playerAudioSource.PlayOneShot(_pickupSound);
            _gameManager.AddScore();
        }
    }
}

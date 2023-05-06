using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //variables declaration
    public bool isGameStarted;
    public bool gameOver;
    private AudioSource _cameraAudioSource;
    private Text _scoreText;
    private Text _gameOverText;
    private int _currentScore = 0;
    private int _scoreAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        //initializing variables
        _cameraAudioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        _scoreText = GameObject.Find("Score").GetComponent<Text>();
        _gameOverText = GameObject.Find("GameOver").GetComponent<Text>();
        //seting current score to 0
        _scoreText.text += _currentScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //set isGameStarted to true
        isGameStarted = true;
    }

    public void GameOver()
    {
        //set gameOver to true, enable gameOver text and stop music
        gameOver = true;
        _gameOverText.enabled = true;
        _cameraAudioSource.Stop();
    }

    public void AddScore()
    {
        //replace previous amount of score with the new one
        string newScoreText = _scoreText.text.Replace(_currentScore.ToString(), (_currentScore + _scoreAmount).ToString());
        //set new amount of score in UI
        _scoreText.text = newScoreText;
        //update currentScore
        _currentScore += _scoreAmount;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    private bool gameOn = false;
    private float score = 0;
    private int gameLevel = 1;

    public float gameSpeed = 1;
    public GameObject playBtn;
    public Text scoreUi;

    void Start()
    {
        NewGame();
    }

    void Update() {
        scoreUi.text = Mathf.FloorToInt(score).ToString();
    }

    public void GameOver() {
        gameOn = false;

        playBtn.SetActive(true);
        playBtn.GetComponentInChildren<Text>().text = "Reset";
    }

    public void NewGame() {
        gameOn = true;
        score = 0;
        gameLevel = 1;
        gameSpeed = 1;

        scoreUi.text = "0";
        playBtn.SetActive(false);

        StartCoroutine(ScoreCounter());
        StartCoroutine(GameSpeedController());
    }

    public bool GameIsOn() {
        return gameOn;
    }

    private IEnumerator ScoreCounter() {
        while (GameIsOn()) {
            score += gameSpeed * 1.0f;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator GameSpeedController() {
        while (GameIsOn() || gameSpeed <= 2) {
            yield return new WaitForSeconds(5);
            gameSpeed += 0.1f;
        }
    }
}

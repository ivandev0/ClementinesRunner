using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    private bool gameOn = false;
    private bool gameEnd = false;
    private float score = 0;

    public int gameLevel = 0;
    public float gameSpeed = 1;
    public GameObject playBtn;
    public Text scoreUi;
    public GameObject finalPanelUi;
    public Text finalScoreUi;

    public int maxGameLevel = 3;

    private Coroutine scoreCounter;
    private Coroutine gameSpeedController;
    private Coroutine gameLevelController;

    void Start()
    {

    }

    void Update() {
        scoreUi.text = Mathf.FloorToInt(score).ToString();
    }

    public void GameOver() {
        gameOn = false;
        gameEnd = true;

        finalPanelUi.SetActive(true);
        finalScoreUi.text = Mathf.FloorToInt(score).ToString();
        playBtn.SetActive(true);

        StopCoroutine(scoreCounter);
        StopCoroutine(gameSpeedController);
        StopCoroutine(gameLevelController);
    }

    public void NewGame() {
        gameOn = true;
        gameEnd = false;

        score = 0;
        scoreUi.text = "0";
        gameLevel = 0;
        gameSpeed = 1;

        playBtn.SetActive(false);
        finalPanelUi.SetActive(false);

        scoreCounter = StartCoroutine(ScoreCounter());
        gameSpeedController = StartCoroutine(GameSpeedController());
        gameLevelController = StartCoroutine(GameLevelController());

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy")) {
            Destroy(enemy);
        }
        foreach (var bullet in GameObject.FindGameObjectsWithTag("Bullet")) {
            Destroy(bullet);
        }
    }

    public bool GameIsOn() {
        return gameOn;
    }

    public bool GameIsOver() {
        return gameEnd;
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

    private IEnumerator GameLevelController() {
        while (GameIsOn() && gameLevel < maxGameLevel) {
            yield return new WaitForSeconds(10);
            gameLevel++;
        }
    }
}

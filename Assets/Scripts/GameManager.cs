using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {
    private bool gameOn = false;
    private bool gameEnd = false;
    private float score = 0;
    private int gameLevel = 1;

    public float gameSpeed = 1;
    public float defaultGravity = 3.0f;
    public GameObject playBtn;
    public Text scoreUi;
    public GameObject finalPanelUi;
    public Text finalScoreUi;

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

    }

    public void NewGame() {
        gameOn = true;
        gameEnd = false;

        score = 0;
        scoreUi.text = "0";
        gameLevel = 1;
        gameSpeed = 1;

        playBtn.SetActive(false);
        finalPanelUi.SetActive(false);

        StartCoroutine(ScoreCounter());
        StartCoroutine(GameSpeedController());

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
}

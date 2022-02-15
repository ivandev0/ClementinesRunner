using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemiesByLevel {
    public GameObject[] enemies;
    public Transform[] spawnPoints;
}

[System.Serializable]
public class Patterns {
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public float delay;
}

public class EnemySpawner : MonoBehaviour {
    public EnemiesByLevel[] enemiesByLevel;
    public Patterns[] patterns;
    public float firstDelay = 2f;
    public float spawnRate = 2f;

    private bool spawning = false;
    static System.Random rnd = new System.Random();

    void Update() {
        if (GameManager.Instance.GameIsOn() && !spawning) {
            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine() {
        spawning = true;
        var spawnPosition = transform;

        yield return new WaitForSeconds(firstDelay);
        while (GameManager.Instance.GameIsOn()) {
            if (GameManager.Instance.gameLevel == GameManager.Instance.maxGameLevel && rnd.NextDouble() >= 0.5f) {
                var pattern = patterns[rnd.Next(patterns.Length)];
                var enemies = pattern.enemies;
                var spawnPoints = pattern.spawnPoints;
                for (var i = 0; i < pattern.enemies.Length; i++) {
                    Instantiate(enemies[i], spawnPoints[i].position, enemies[i].transform.rotation, spawnPosition);
                    yield return new WaitForSeconds(pattern.delay);
                }
            } else {
                var enemies = enemiesByLevel[GameManager.Instance.gameLevel].enemies;
                var spawnPoints = enemiesByLevel[GameManager.Instance.gameLevel].spawnPoints;
                var index = rnd.Next(enemies.Length);
                var enemy = enemies[index];
                Instantiate(enemy, spawnPoints[index].position, enemy.transform.rotation, spawnPosition);
            }

            yield return new WaitForSeconds(spawnRate);
        }

        spawning = false;
    }
}

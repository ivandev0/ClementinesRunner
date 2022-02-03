using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject[] enemies;
    public Transform[] spawnPoints;
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
            var index = rnd.Next(enemies.Length);
            var enemy = enemies[index];
            Instantiate(enemy, spawnPoints[index].position, enemy.transform.rotation, spawnPosition);
            yield return new WaitForSeconds(spawnRate);
        }

        spawning = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject[] enemies;
    public float firstDelay = 2f;
    public float spawnRate = 2f;

    private bool spawning = false;
    void Start() {
    }

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
            GameObject.Instantiate(enemies[0], spawnPosition.position, Quaternion.identity, spawnPosition);
            yield return new WaitForSeconds(spawnRate);
        }

        spawning = false;
    }
}

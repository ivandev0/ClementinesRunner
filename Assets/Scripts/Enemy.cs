using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 10.0f;
    private Vector2 target;

    void Start() {
        target = new Vector2(-100.0f, transform.position.y);
    }

    void Update() {
        if (!GameManager.Instance.GameIsOn()) {
            Destroy(gameObject);
            return;
        }

        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        } else if (col.gameObject.CompareTag("Bullet")) {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}

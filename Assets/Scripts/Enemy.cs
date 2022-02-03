using UnityEngine;

public class Enemy : MonoBehaviour {
    public float speed = 10.0f;
    private float target = -100;

    void Update() {
        if (!GameManager.Instance.GameIsOn()) {
            Destroy(gameObject);
            return;
        }

        var step = speed * Time.deltaTime;
        var xPosition = Vector2.MoveTowards(new Vector2(transform.position.x, 0), new Vector2(target, 0), step).x;
        transform.position = new Vector2(xPosition, transform.position.y);
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

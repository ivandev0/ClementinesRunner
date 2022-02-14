using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour {
    public float speed = 10.0f;
    private float target = -100;
    private bool isDead = false;

    public abstract bool DestroyOnBulletCollision { get; }

    void Update() {
        if (!GameManager.Instance.GameIsOn()) {
            Destroy(gameObject);
            return;
        }

        if (isDead) return;
        var step = speed * Time.deltaTime;
        var xPosition = Vector2.MoveTowards(new Vector2(transform.position.x, 0), new Vector2(target, 0), step).x;
        transform.position = new Vector2(xPosition, transform.position.y);
    }

    public void Die() {
        isDead = true;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = 5f * new Vector2(0.5f, 0.866f);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Player")) {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }
}

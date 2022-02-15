using System;
using System.Collections;
using UnityEngine;

namespace Enemies {
    public abstract class AbstractEnemy : MonoBehaviour {
        public float speed = 10.0f;
        private float target = -100;
        private bool isDead = false;

        protected Animator animator;
        public abstract bool DestroyOnBulletCollision { get; }

        protected void Start() {
            animator = GetComponent<Animator>();
            StartCoroutine(DieTimer());
        }

        protected void Update() {
            if (!GameManager.Instance.GameIsOn()) {
                animator.speed = 0;
                return;
            }

            animator.speed = 1;
            if (isDead) return;
            var step = GameManager.Instance.gameSpeed * speed * Time.deltaTime;
            var xPosition = Vector2.MoveTowards(new Vector2(transform.position.x, 0), new Vector2(target, 0), step).x;
            transform.position = new Vector2(xPosition, transform.position.y);
        }

        public void Die() {
            isDead = true;
            GetComponent<Collider2D>().enabled = false;
            var rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.velocity = 5f * new Vector2(0.5f, 0.866f);
            rigidbody2D.gravityScale = 3f;
        }

        protected void OnCollisionEnter2D(Collision2D col) {
            if (col.gameObject.CompareTag("Player")) {
                GameManager.Instance.GameOver();
            }
        }

        private IEnumerator DieTimer() {
            yield return new WaitForSeconds(20);
            Destroy(gameObject);
        }
    }
}

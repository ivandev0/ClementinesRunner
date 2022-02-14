using System;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public float distanceBeforeHit = 2f;
	public static float bulletForce = 200f;

	private Animator animator;
	private Rigidbody2D rigidbody2D;
	private static readonly int almostHit = Animator.StringToHash("AlmostHit");
	private static readonly int hit = Animator.StringToHash("Hit");
	private static readonly int dead = Animator.StringToHash("Dead");

	void Start () {
		animator = GetComponent<Animator>();
		rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void Update() {
		var hits = Physics2D.RaycastAll(transform.position, Vector2.right, float.MaxValue);
		foreach (var raycastHit2D in hits) {
			if (raycastHit2D.collider.gameObject.CompareTag("Enemy")) {
				if (raycastHit2D.distance <= distanceBeforeHit) {
					animator.SetBool(almostHit, true);
				}
				break;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.CompareTag("Enemy")) {
			var enemy = collision.gameObject.GetComponent<AbstractEnemy>();
			if (enemy.DestroyOnBulletCollision) {
				Destroy(collision.gameObject);
			}
			animator.SetBool(hit, true);
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.gravityScale = 9.8f;

			GetComponent<CircleCollider2D>().enabled = false;
			GetComponent<BoxCollider2D>().enabled = true;
		} else if (collision.gameObject.CompareTag("Ground")) {
			animator.SetBool(dead, true);

			rigidbody2D.gravityScale = 0;
			rigidbody2D.AddForce(bulletForce * 2f * Vector2.left);
			GetComponent<BoxCollider2D>().isTrigger = true;
		}
	}
}
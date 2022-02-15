using UnityEngine;

namespace Enemies {
	public class JumpingEnemy : Enemy {
		public LayerMask groundLayer;
		private bool grounded = false;

		private void FixedUpdate() {
			if (!GameManager.Instance.GameIsOn()) {
				animator.speed = 0;
				var rigidbody2D = GetComponent<Rigidbody2D>();
				rigidbody2D.gravityScale = 0;
				rigidbody2D.isKinematic = true;
				rigidbody2D.velocity = Vector2.zero;
				return;
			}

			if (grounded && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.999f) {
				animator.Play("JumpingEnemy_up", 0, 0);
				grounded = false;
			}
		}

		private void OnCollisionEnter2D(Collision2D col) {
			base.OnCollisionEnter2D(col);

			if (1 << col.gameObject.layer == groundLayer) {
				grounded = true;
				animator.Play("JumpingEnemy_ground", 0, 0);
			}
		}
	}
}

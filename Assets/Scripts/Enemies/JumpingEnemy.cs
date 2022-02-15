using UnityEngine;

namespace Enemies {
	public class JumpingEnemy : Enemy {
		public LayerMask groundLayer;
		private bool grounded = false;

		private Animator animator;

		private void Start() {
			animator = GetComponent<Animator>();
		}

		private void FixedUpdate() {
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

using UnityEngine;

public class Enemy : AbstractEnemy {
	protected override bool DestroyOnBulletCollision => true;
}

using UnityEngine;

public class Enemy : AbstractEnemy {
	public override bool DestroyOnBulletCollision => true;
}

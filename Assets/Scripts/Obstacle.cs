using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : AbstractEnemy {
	public override bool DestroyOnBulletCollision => false;
}

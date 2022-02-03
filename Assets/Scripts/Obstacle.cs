using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : AbstractEnemy {
	protected override bool DestroyOnBulletCollision => false;
}

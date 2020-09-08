using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHandle : MonoBehaviour
{
    public float ShootSpeed;

	public void FixedUpdate()
	{
		ObjectPool.Instance.Spawn(PoolType.Bullet, Vector3.zero, Quaternion.identity);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public void FixedUpdate()
	{
		transform.position += new Vector3(1, 0) * Time.fixedDeltaTime;
	}
}

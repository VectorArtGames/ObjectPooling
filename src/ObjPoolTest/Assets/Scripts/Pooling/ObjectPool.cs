using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	public const int POOL_SIZE = 100;

	[Serializable]
	public struct Pool
	{
		public PoolType Type;
		public GameObject Prefab;
		public int Size;
	}

	public List<Pool> Pools;
	public Dictionary<PoolType, Queue<GameObject>> ObjectPoolDictionary;

	#region Singleton

	public static ObjectPool Instance { get; private set; }

	public void Awake()
	{
		Instance = this;
	}

	#endregion

	public void Start()
	{
		ObjectPoolDictionary = 
			new Dictionary<PoolType, Queue<GameObject>>();

		var TopParent = new GameObject($"[+] Pools");

		foreach (var p in Pools)
		{
			var parent = new GameObject($"Pool: {Enum.GetName(p.Type.GetType(), p.Type)}");
			parent.transform.SetParent(TopParent.transform);
			var pool = new Queue<GameObject>();
			for (var i = 0; i < p.Size; i++)
			{
				var obj = Instantiate(p.Prefab, parent.transform);
				obj.SetActive(false);
				pool.Enqueue(obj);
			}

			ObjectPoolDictionary.Add(p.Type, pool);
		}
	}

	public GameObject Spawn(PoolType Type, Vector3 Position, Quaternion Rotation)
	{
		if (!ObjectPoolDictionary.ContainsKey(Type))
		{
			Debug.LogWarning($"No Object contains in pool of type: {Enum.GetName(Type.GetType(), Type)}");
			return null;
		}

		var obj = ObjectPoolDictionary[Type].Dequeue();
		obj.transform.position = Position;
		obj.transform.rotation = Rotation;
		obj.SetActive(true);

		ObjectPoolDictionary[Type].Enqueue(obj);

		return obj;
	}
}

public enum PoolType
{
	Bullet,
	Other
}
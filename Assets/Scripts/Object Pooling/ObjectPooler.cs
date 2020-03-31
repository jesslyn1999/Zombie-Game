using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
	public string tag;
	public GameObject objectToPool;
	public int amountToPool;
	public bool shouldExpand = true;

	public ObjectPoolItem(string tags, GameObject obj, int amt, bool exp = true)
	{
		tag = tags;
		objectToPool = obj;
		amountToPool = Mathf.Max(amt, 2);
		shouldExpand = exp;
	}
}

public class ObjectPooler : MonoBehaviour
{

	public List<ObjectPoolItem> itemsToPool;

	public Dictionary<string, Queue<GameObject>> pooledObjectsDict;

    #region Singleton

    public static ObjectPooler SharedInstance;
	private void Awake()
	{
		SharedInstance = this;
	}

    #endregion


    private void Start()
	{
		pooledObjectsDict = new Dictionary<string, Queue<GameObject>>();


		for (int i = 0; i < itemsToPool.Count; i++)
		{
			ObjectPoolItemToPooledObject(i);
		}
	}

	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
	{
		if(pooledObjectsDict == null)
		{
			Debug.Log("u RE the error");
		}
		if (!pooledObjectsDict.ContainsKey(tag))
		{
			Debug.LogWarning("Pool with tag=" + tag + " doesn't exist.");
			return null;
		}

		GameObject objectToSpawn = pooledObjectsDict[tag].Dequeue();

		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

		IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

		if (pooledObj != null)
		{
			pooledObj.OnObjectSpawn();
		}

		pooledObjectsDict[tag].Enqueue(objectToSpawn);

		return objectToSpawn;
	}

	public Queue<GameObject> GetAllPooledObjects(string tag)
	{
		return pooledObjectsDict[tag];
	}


	public int AddObject(string tag, GameObject GO, int amt = 3, bool exp = true)
	{
		ObjectPoolItem item = new ObjectPoolItem(tag, GO, amt, exp);
		int currLen = itemsToPool.Count;
		itemsToPool.Add(item);
		ObjectPoolItemToPooledObject(currLen);
		return currLen;
	}


	void ObjectPoolItemToPooledObject(int index)
	{
		ObjectPoolItem item = itemsToPool[index];
		Queue<GameObject> objectPoolQueue = new Queue<GameObject>();

		//pooledObjects = new List<GameObject>();
		for (int i = 0; i < item.amountToPool; i++)
		{
			GameObject obj = (GameObject)Instantiate(item.objectToPool);
			obj.SetActive(false);
			obj.transform.parent = this.transform;
			//pooledObjects.Add(obj);
			objectPoolQueue.Enqueue(obj);
		}
		pooledObjectsDict.Add(item.tag, objectPoolQueue);

	}
}

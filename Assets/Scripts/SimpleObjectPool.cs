﻿using UnityEngine;
using System.Collections.Generic;

// осмысленно взято отсюда https://unity3d.com/ru/learn/tutorials/topics/user-interface-ui/button-prefab?playlist=17111

// пул кнопок

//TODO rename
public class SimpleObjectPool : MonoBehaviour
{
	// the prefab that this object pool returns instances of
	public GameObject prefab;

	// collection of currently inactive instances of the prefab
	private Stack<GameObject> inactiveInstances = new Stack<GameObject>();

	// Returns an instance of the prefab
	public GameObject GetObject(Transform parentGameObject) 
	{
		GameObject spawnedGameObject;

		// if there is an inactive instance of the prefab ready to return, return that
		if (inactiveInstances.Count > 0) 
		{
			// remove the instance from teh collection of inactive instances
			spawnedGameObject = inactiveInstances.Pop();
		}

		// otherwise, create a new instance
		else 
		{
			spawnedGameObject = (GameObject)GameObject.Instantiate(prefab);
			// add the PooledObject component to the prefab so we know it came from this pool
			PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject>();
			pooledObject.pool = this;
		}

		// устанавливаем родителя для spawnedGameObject. Если использовать SetParent  с одним аргументом - сломает скейлинг. 
        // Второй аргумент "false" говорит "dont modify parent-relative position"
		spawnedGameObject.transform.SetParent(parentGameObject, false);

		//grid layout родителя иногда ломает скейл дочерней кнопки. Фиксим принудительно.
		spawnedGameObject.transform.localScale = new Vector3 (1,1,1);

		// и наконец включаем
		spawnedGameObject.SetActive(true);

		// return a reference to the instance
		return spawnedGameObject;
	}

	// Return an instance of the prefab to the pool
	public void ReturnObject(GameObject toReturn) 

	{
		PooledObject pooledObject = toReturn.GetComponent<PooledObject>();

		// if the instance came from this pool, return it to the pool
		if(pooledObject != null && pooledObject.pool == this)
		{
			// make the instance a child of this and disable it
			toReturn.transform.SetParent(transform,false);
            toReturn.GetComponent<ItemButton>().TuneButtonForPool();
            toReturn.SetActive(false);

			// add the instance to the collection of inactive instances
			inactiveInstances.Push(toReturn);
		}

		// otherwise, just destroy it
		else
		{
			Debug.LogWarning(toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
			Destroy(toReturn);
		}
	}
}

// a component that simply identifies the pool that a GameObject came from
public class PooledObject : MonoBehaviour
{
	public SimpleObjectPool pool;
}
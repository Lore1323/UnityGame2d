using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolManager : MonoBehaviour
{
    public static List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>();

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        PooledObjectInfo pool = null;
        foreach (PooledObjectInfo p in ObjectPools)
        {
            if (p.LookupString == objectToSpawn.name)
            {
                pool = p;
                break;
            }
        }

        if (pool == null)
        {
            pool = new PooledObjectInfo() { LookupString = objectToSpawn.name };
            ObjectPools.Add(pool);
        }


        GameObject spawnableObj = null;
        foreach (GameObject obj in pool.InactiveObjects)
        {
            if (obj != null)
            {
                spawnableObj = obj;
                break;
            }
        }

        if (spawnableObj == null)
        {
            spawnableObj = Instantiate(objectToSpawn, spawnPosition, spawnRotation);
        }

        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.InactiveObjects.Remove(spawnableObj);
            spawnableObj.SetActive(true);
        }
        return spawnableObj;
    }

    public static void ReturnObjectToPool(GameObject obj)
    {
        string goName = obj.name.Replace("(Clone)", string.Empty);
        PooledObjectInfo pool = ObjectPools.Find(p => p.LookupString == goName);

        if (pool == null)
        {
           
        }

        else
        {
            obj.SetActive(false);
            pool.InactiveObjects.Add(obj);
        }
            
    }
    
    public class PooledObjectInfo
    {
        public string LookupString;
        public List<GameObject> InactiveObjects = new List<GameObject>();
        
    }
}



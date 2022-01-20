using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;


/// <summary>
/// USED TO SPAWN BLOOD POOL SPRITES UPON PARTICLE COLLISION THROUGH OBJECT POOLER.
/// 
/// This script is a modified version of the object pooler I previously created.
/// 
/// The BloodObjectPooler is meant to be the only object pooler in the scene.
/// It is not ideal to have multiple object poolers running at the same time. 
/// If there is another object pooler in your scene, you may want 
/// to modify/combine this script into another object pooler instance for effeciency.
/// 
/// Nick Carlson 12/18/2020
/// </summary>
public class BloodObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string key; // Dictionary Key string
        public GameObject prefab; // The game object to be pooled
        public int size;// starting amount of prefabs to load in this pool.
        public int maxSize;//Maximum amount of prefabs in this pool. Pool does not dynamically grow if maxSize <= size.
    }

    public static BloodObjectPooler Instance; // For Singleton
    public List<Pool> pools; //All the pools of objects to be spawned
    public Dictionary<string, Queue<GameObject>> poolDictionary; //A dictionary containing all the game objects in queues - the object pools. 

    [SerializeField]
    string[] BloodSplatterKeys;

    public string getRandomBloodSplatterKey()
    {
        int randomSprite = UnityEngine.Random.Range(0, BloodSplatterKeys.Length);
        return BloodSplatterKeys[randomSprite];

    }

    public void Awake()
    {
        Instance = this;

        BloodSplatterKeys = new string[pools.Count];

        for (int i = 0; i < pools.Count; i++)
        {
            BloodSplatterKeys[i] = pools[i].key;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, this.gameObject.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.key, objectPool);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Spawn Function + Overrides
    /// <summary>
    /// Returns an inactive spawned object, specified by the key parameter, at its last known position & orientation.
    /// This method is called inside all the overides, as it handles Dequeuing and Enqueing from the Pools, as well as resizing them if needed.
    /// If all assets in the pool are used, and the pool is unable to grow in size, then nothing is spawned.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>Returns the spawned game object. Returns null if unable to spawn object.</returns>
    public GameObject SpawnObject(string key)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogWarning("Pool with key " + key + " not found.");
            return null;
        }

        //Peek to see if the next object in the queue is still active. If so, grow the queue?
        GameObject objectToSpawn = poolDictionary[key].Dequeue();

        if (objectToSpawn.activeSelf) // If the object is active, its not ready to be reused yet
        {
            poolDictionary[key].Enqueue(objectToSpawn); //re-queue the asset that is in use
            objectToSpawn = ResizePool(key); //attempt to increase the object pool.

            if (objectToSpawn == null)
            {
                Debug.LogWarning("All assets in this pool are used, unable to resize pool.");
                return null;
            }
        }
        else
        {
           objectToSpawn.SetActive(false);
           poolDictionary[key].Enqueue(objectToSpawn);
        }

       
        return objectToSpawn;
    }

    /// <summary>
    /// Returns an active spawned object, specified by the key parameter, at a specified position with its last known orientation
    /// </summary>
    /// <param name="key"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject SpawnObject(string key, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogWarning("Pool with key " + key + " not found.");
            return null;
        }

        GameObject objectToSpawn = SpawnObject(key);
        if (objectToSpawn != null)
        {
            objectToSpawn.transform.position = position;
            objectToSpawn.SetActive(true);
            return objectToSpawn;
        }
        else return null;

    }

    /// <summary>
    /// Returns a spawned object, specified by the key parameter, at a specified orientation with its last known position
    /// </summary>
    /// <param name="key"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject SpawnObject(string key, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogWarning("Pool with key " + key + " not found.");
            return null;
        }

        GameObject objectToSpawn = SpawnObject(key);
        if (objectToSpawn != null)
        {
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);
            return objectToSpawn;
        }
        else return null;
    }

    /// <summary>
    /// Returns a spawned object, specified by the key parameter, at a specified position and orientation
    /// </summary>
    /// <param name="key">Identifies which pool to spawn an object from. </param>
    /// <param name="position">The position of the spawned object.</param>
    /// <param name="rotation">The orientation of the spawned object.</param>
    /// <returns></returns>
    public GameObject SpawnObject(string key, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(key))
        {
            Debug.LogWarning("Pool with key " + key + " not found.");
            return null;
        }

        GameObject objectToSpawn = SpawnObject(key);
        if (objectToSpawn != null)
        {
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;
            objectToSpawn.SetActive(true);
            return objectToSpawn;
        }
        else return null;
    }

    /// <summary>
    /// Determines if the pool with the specified key is full. 
    /// If the pool is full, and able to be resized, the size of the pool will be increased.
    /// </summary>
    /// <param name="key">Used to identify which pool should be resized.</param>
    /// <returns>Returns the newly created game object, returns null if unable to resize pool.</returns>
    public GameObject ResizePool(string key)
    {
        foreach (var Pool in pools)
        {
            if (Pool.key == key)
            {
                if (Pool.size < Pool.maxSize)
                {
                    GameObject obj = Instantiate(Pool.prefab, this.gameObject.transform);
                    obj.SetActive(false);
                    poolDictionary[key].Enqueue(obj);
                    Pool.size++;
                    return obj;
                }
                else
                {
                    return null; // Pool is at max size or not set to expandable!
                }
            }
        }
        Debug.LogWarning("Pool with key : " + key + " not found!");
        return null;
    }

    /// <summary>
    /// TODO create a function that removes objects from pools, to set them back to their starting size?
    /// </summary>
    public void CleanPools()
    {



    }

    #endregion

}

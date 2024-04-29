using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace TT
{
    public class ObjectPool : IGameService
    {
        static Dictionary<string, Tuple<Transform, LinkedList<Component>>> _pools
            = new Dictionary<string, Tuple<Transform, LinkedList<Component>>>();

        public Transform CreatePool<T>(string poolName) where T : Component
        {
            Transform pool = new GameObject("[Pool]: " + poolName).transform;
            pool.position = Vector3.zero;

            var poolHolder = new Tuple<Transform, LinkedList<Component>>(pool, new LinkedList<Component>());

            _pools.Add(poolName, poolHolder);
            return pool;
        }

        public Transform CreatePool<T>(string poolName, T prefab, int size) where T : Component
        {
            if (_pools.ContainsKey(poolName))
            {
                return _pools[poolName].Item1;
            }

            LinkedList<Component> objects = new LinkedList<Component>();

            Transform pool = new GameObject("[Pool]: " + poolName).transform;
            pool.position = Vector3.zero;
            for (int i = 0; i < size; ++i)
            {
                T newObj = GameObject.Instantiate(prefab, pool);
                newObj.gameObject.name = prefab.name;
                newObj.gameObject.SetActive(false);
                objects.AddLast(newObj);
            }

            _pools.Add(poolName, new Tuple<Transform, LinkedList<Component>>(pool, objects));

            return pool;
        }

        public T GetObject<T>(string poolName) where T : Component
        {
            if (!_pools.ContainsKey(poolName))
            {
                Debug.LogWarning($"GetObject failed! Because PoolName: {poolName} is not exists!");
                return null;
            }

            LinkedList<Component> objects = _pools[poolName].Item2;

            foreach (Component obj in objects)
            {
                if (obj != null && !obj.gameObject.activeSelf)
                {
                    obj.gameObject.SetActive(true);
                    return obj as T;
                }
            }
            return null;
        }

        public T GetRandomObject<T>(string poolName) where T : Component
        {
            if (!_pools.ContainsKey(poolName))
            {
                Debug.LogWarning($"GetObject failed! Because PoolName: {poolName} is not exists!");
                return null;
            }

            LinkedList<Component> objects = _pools[poolName].Item2;
            LinkedList<Component> suiableObjects = new LinkedList<Component>();
            foreach (Component obj in objects)
            {
                if (obj != null && !obj.gameObject.activeSelf)
                {
                    suiableObjects.AddLast(obj);
                }
            }

            if (suiableObjects.Count == 0) return null;

            int randIndex = UnityEngine.Random.Range(0, suiableObjects.Count);

            T result = suiableObjects.ElementAt(randIndex) as T;
            result.gameObject.SetActive(true);
            return result;
        }


        public bool AddMoreObject(string poolName, int amount)
        {
            if (!_pools.ContainsKey(poolName))
            {
                Debug.LogWarning($"AddMoreObject failed! Because PoolName: {poolName} is not exists!");
                return false;
            }

            Component prefab = null;
            LinkedList<Component> objects = _pools[poolName].Item2;
            foreach (Component obj in objects)
            {
                if (obj != null)
                {
                    prefab = obj;
                    break;
                }
            }
            if (prefab == null)
            {
                return false;
            }

            Transform pool = _pools[poolName].Item1;
            for (int i = 0; i < amount; i++)
            {
                Component newObj = GameObject.Instantiate(prefab, pool);
                newObj.gameObject.name = prefab.name;
                newObj.gameObject.SetActive(false);
                objects.AddLast(newObj);
            }

            return true;
        }

        public bool AddObject<T>(string poolName, T obj) where T : Component
        {
            if (!_pools.ContainsKey(poolName))
            {
                Debug.LogWarning($"AddMoreObject failed! Because PoolName: {poolName} is not exists!");
                return false;
            }

            obj.transform.SetParent(_pools[poolName].Item1);
            _pools[poolName].Item2.AddLast(obj);
            return true;
        }

        public void DestroyPool(string poolName)
        {
            if (!_pools.ContainsKey(poolName))
            {
                Debug.LogWarning($"DestroyPool failed! Because PoolName: {poolName} is not exists!");
                return;
            }

            Transform pool = _pools[poolName].Item1;
            GameObject.Destroy(pool.gameObject);
        }
    }
}

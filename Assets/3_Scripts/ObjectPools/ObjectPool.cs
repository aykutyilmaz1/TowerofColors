using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _3_Scripts
{
    public class ObjectPool<T> : MonoBehaviour where T : Component
    {
        public List<T> prefabs;
        public int poolSize = 10;

        public List<T> _objectPool = new();

        void Start()
        {
            InitializeObjectPool();
        }

        void InitializeObjectPool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                T obj = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(transform);
                _objectPool.Add(obj);
            }
        }

        public T GetObjectFromPool()
        {
            foreach (T obj in _objectPool)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }

            // If no inactive object is found, create a new one
            T newObj = Instantiate(prefabs[Random.Range(0, prefabs.Count)]);
            _objectPool.Add(newObj);
            return newObj;
        }

        public virtual void ReturnObjectToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
        }
        
        public virtual void ResetPool()
        {
            _objectPool.RemoveAll(item => item == null);
            
            foreach (T obj in _objectPool)
            {
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(transform);
            }
        }
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.GridGame
{
    public class ObjectPool : MonoBehaviour
    {
        public static ObjectPool SharedInstance;
        public List<GameObject> pooledObjects;
        public GameObject objectToPool;
        public int amountToPool;
        private int _activeAmountToPool;

        private void Awake()
        {
            SharedInstance = this;
        }

        private void Start()
        {
            _activeAmountToPool = amountToPool;

            pooledObjects = new List<GameObject>();
            for (var i = 0; i < amountToPool; i++)
            {
                var tmp = Instantiate(objectToPool, transform);
                tmp.SetActive(false);
                pooledObjects.Add(tmp);
            }
        }

        public GameObject GetPooledObject()
        {
            if (_activeAmountToPool <= 0)
            {
                var tmp = Instantiate(objectToPool, transform);
                tmp.SetActive(true);
                pooledObjects.Add(tmp);
                amountToPool++;
                return tmp;
            }
            else
            {
                for (var i = 0; i < amountToPool; i++)
                {
                    if (!pooledObjects[i].activeInHierarchy)
                    {
                        pooledObjects[i].SetActive(true);
                        _activeAmountToPool--;
                        return pooledObjects[i];
                    }
                }
            }


            return null;
        }

        public void SetPooledObject(GameObject pooledObject)
        {
            pooledObject.SetActive(false);
            _activeAmountToPool++;
        }
    }
}
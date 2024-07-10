using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectPool : MonoBehaviour
{
  public static ObjectPool SharedInstance;
  [SerializeField] private List<GameObject> _poolObjects;
  [SerializeField] private GameObject _objectToPoolPrefab;
  [SerializeField] private int _amountToPool;

  private void Awake()
  {
    SharedInstance = this;
  }

  private void Start()
  {
    if (_poolObjects == null)
    {
      _poolObjects = new List<GameObject>();
    }
    else
    {
      _poolObjects.Clear();
    }

    for (int i = 0; i < _amountToPool; i++)
    {
      GameObject poolTemp = Instantiate(_objectToPoolPrefab);
      poolTemp.SetActive(false);
      _poolObjects.Add(poolTemp);
    }
  }

  public GameObject GetPoolesObject()
  {
    for (int i = 0; i < _amountToPool; i++)
    {
      if (!_poolObjects[i].activeInHierarchy)
      {
        return _poolObjects[i];
      }
    }

    return null;
  }
}



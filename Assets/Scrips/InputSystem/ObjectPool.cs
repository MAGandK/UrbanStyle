using System.Collections.Generic;
using UnityEngine;


public class ObjectPool : MonoBehaviour
{
  public static ObjectPool SharedInstance;
  
  [SerializeField] private List<GameObject> _poolObjects;
  [SerializeField] private GameObject _objectToPoolPrefab;
  [SerializeField] private int _amountToPool;

  private void Awake()
  {
    if (SharedInstance == null)
    {
      SharedInstance = this;
    }
    else
    {
      Destroy(gameObject);
    }
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



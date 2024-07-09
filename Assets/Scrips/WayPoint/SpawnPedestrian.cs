using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnPedestrian : MonoBehaviour
{
    [SerializeField] private GameObject[] _pedestrianPrefab;
    [SerializeField] private int _pedestrianCount;
   
    private void Start()
    {
        StartCoroutine(Spawn());
    }
   
   IEnumerator Spawn()
    {
        int count = 0;
        while (count < _pedestrianCount)
        {
            GameObject obj = Instantiate(_pedestrianPrefab[Random.Range(0, _pedestrianPrefab.Length)]);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            Waypoint waypoint = child.GetComponent<Waypoint>();
            obj.GetComponent<WaypointNavigator>()._currentWaypoint = waypoint;
            obj.transform.position = child.position;
   
            // obj.transform.position = new Vector3(obj.transform.position.x, 0,obj.transform.position.z);
            yield return new WaitForEndOfFrame();
            count++;
           
        }
    }
}

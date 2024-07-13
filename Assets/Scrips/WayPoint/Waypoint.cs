using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Waypoint : MonoBehaviour
{
   public Waypoint _previousWaypoint;
   public Waypoint _nextWaypoint;

    [Range(0, 5f)] public float _width = 1f;
    [Range(0, 1f)] public float _branchRatio = 0.5f;

    public List<Waypoint> Branches;

    public Vector3 GetPosition()
    {
        Vector3 minBounds = transform.position + (transform.right * _width / 2f);
        Vector3 maxBounds = transform.position - (transform.right * _width / 2f);
        
        return Vector3.Lerp(minBounds,maxBounds,Random.Range(0f, 1f));
    }
}

using UnityEngine;

public class Waypoint : MonoBehaviour
{
   public Waypoint _previosWaypoint;
   public Waypoint _nextWaypoint;

    [Range(0, 5f)] public float _width = 1f;

    public Vector3 GetPosition()
    {
        Vector3 minBounds = (transform.position + transform.right * _width) / 2f;
        Vector3 maxBounds = (transform.position - transform.right * _width) / 2f;

        return Vector3.Lerp(minBounds, maxBounds, Random.Range(0f, 1f));
    }
}

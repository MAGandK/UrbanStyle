using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor 
{
  [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
  public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
  {
    if ((gizmoType & GizmoType.Selected) !=0)
    {
      Gizmos.color = Color.yellow;
      
    }
    else
    {
      Gizmos.color = Color.yellow * 0.5f;
    }
    
    Gizmos.DrawSphere(waypoint.transform.position,0.1f);
    Gizmos.color = Color.white;
    
    Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint._width / 2f),
                    waypoint.transform.position - (waypoint.transform.right * waypoint._width /2f));

    if (waypoint._previosWaypoint != null)
    {
      Gizmos.color = Color.red;
      Vector3 offset = waypoint.transform.right * waypoint._width / 2f;
      Vector3 offsetTo = waypoint._previosWaypoint.transform.right * waypoint._previosWaypoint._width / 2f;
      
      Gizmos.DrawLine(waypoint.transform.position + offset, waypoint._previosWaypoint.transform.position + offsetTo);
    }

    if (waypoint._nextWaypoint != null)
    {
      Gizmos.color = Color.green;
      Vector3 offset = waypoint.transform.right * -waypoint._width / 2f;
      Vector3 offsetTo = waypoint._nextWaypoint.transform.right * -waypoint._nextWaypoint._width / 2f;
      
      Gizmos.DrawLine(waypoint.transform.position + offset, waypoint._nextWaypoint.transform.position + offsetTo);
    }

    if (waypoint.Branches != null)
    {
      foreach (Waypoint branch in waypoint.Branches)
      {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
      }
    }
  }
}

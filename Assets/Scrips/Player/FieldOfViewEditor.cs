using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;
        Handles.color = Color.green;
        float thickness = 2.0f;
        
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 
            360,fov.ViewRadius, thickness);

        Vector3 viewAngleA = fov.DirectionFromAngle(-fov.viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirectionFromAngle(fov.viewAngle / 2, false);
        
        Handles.color = Color.blue;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.ViewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.ViewRadius);
        
        Handles.color = Color.red;
        foreach (var visibleTarget in fov.VisibleTarget)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.transform.position,thickness);
        }
    }
}

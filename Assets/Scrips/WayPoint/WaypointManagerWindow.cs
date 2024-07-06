using UnityEditor;
using UnityEngine;

public class WaypointManagerWindow : EditorWindow
{
   [MenuItem("Tools/Waypoint Editor")]

   public static void Open()
   {
      GetWindow<WaypointManagerWindow>();
   }

   public Transform waypointRoot;

   private void OnGUI()
   {
      SerializedObject obj = new SerializedObject(this);
      EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

      if (waypointRoot == null)
      {
         EditorGUILayout.HelpBox("Root transform musy be Selection. Please assign a root transform",
            MessageType.Warning);
      }
      else
      {
         EditorGUILayout.BeginVertical("box");
         DrawButtons();
         EditorGUILayout.EndVertical();
      }
      obj.ApplyModifiedProperties();
   }

   private void DrawButtons()
   {
      if (GUILayout.Button("Create Waypoint"))
      {
         CreateWaypoint();
      }

      if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
      {
         if (GUILayout.Button("Create Waypoint Before"))
         {
            CreateWaypointBefore();
         }

         if (GUILayout.Button("Create Waypoint After"))
         {
            CreateWaypointAfter();
         }

         if (GUILayout.Button("Remove Waypoint"))
         {
            RemoveWaypoint();
         }

         if (GUILayout.Button("Add branch Waypoint"))
         {
            AddBranch();
         }
      }
   }

   private void AddBranch()
   {
      GameObject obj = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
      obj.transform.SetParent(waypointRoot,false);

      Waypoint waypoint = obj.GetComponent<Waypoint>();

      Waypoint branchesFrom = Selection.activeGameObject.GetComponent<Waypoint>();
      branchesFrom.Branches.Add(waypoint);

      waypoint.transform.position = branchesFrom.transform.position;
      waypoint.transform.forward = branchesFrom.transform.forward;

      Selection.activeGameObject = waypoint.gameObject;
   }

   private void RemoveWaypoint()
   {
      Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

      if (selectedWaypoint._nextWaypoint != null)
      {
         selectedWaypoint._nextWaypoint._previousWaypoint = selectedWaypoint._previousWaypoint;
      }

      if (selectedWaypoint._previousWaypoint != null)
      {
         selectedWaypoint._previousWaypoint._nextWaypoint = selectedWaypoint._nextWaypoint;
         Selection.activeGameObject = selectedWaypoint._previousWaypoint.gameObject;
      }
      
      DestroyImmediate(selectedWaypoint.gameObject);
   }

   private void CreateWaypointAfter()
   {
      GameObject waypointObject = new GameObject("Waipoint " + waypointRoot.childCount, typeof(Waypoint));
      waypointObject.transform.SetParent(waypointRoot,false);

      Waypoint newWypoint = waypointObject.GetComponent<Waypoint>();

      Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

      waypointObject.transform.position = selectedWaypoint.transform.position;
      waypointObject.transform.forward = selectedWaypoint.transform.forward;

      newWypoint._previousWaypoint = selectedWaypoint;
      
      if(selectedWaypoint._nextWaypoint != null)
      {
         selectedWaypoint._nextWaypoint._previousWaypoint = newWypoint;
         newWypoint._nextWaypoint = selectedWaypoint._nextWaypoint;
      }

      selectedWaypoint._nextWaypoint = newWypoint;
      newWypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
      Selection.activeGameObject = newWypoint.gameObject;
   }

   private void CreateWaypointBefore()
   {
      GameObject waypointObject = new GameObject("Waipoint " + waypointRoot.childCount, typeof(Waypoint));
      waypointObject.transform.SetParent(waypointRoot,false);

      Waypoint newWaypoint = waypointObject.GetComponent<Waypoint>();

      Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

      waypointObject.transform.position = selectedWaypoint.transform.position;
      waypointObject.transform.forward = selectedWaypoint.transform.forward;

      if (selectedWaypoint._previousWaypoint != null)
      {
         newWaypoint._previousWaypoint = selectedWaypoint._previousWaypoint;
         selectedWaypoint._previousWaypoint._nextWaypoint = newWaypoint;
      }

      newWaypoint._nextWaypoint = selectedWaypoint;
      selectedWaypoint._previousWaypoint = newWaypoint;
      newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());
      Selection.activeGameObject = newWaypoint.gameObject;
   }

   private void CreateWaypoint()
   {
      GameObject waypointObject = new GameObject("Waipoint" + waypointRoot.childCount, typeof(Waypoint));
      waypointObject.transform.SetParent(waypointRoot, false);
      Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
      
      if (waypointRoot.childCount > 1)
      {
         waypoint._previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
         waypoint._previousWaypoint._nextWaypoint = waypoint;

         waypoint.transform.position = waypoint._previousWaypoint.transform.position;
         waypoint.transform.forward = waypoint._previousWaypoint.transform.forward;
      }
      
      Selection.activeGameObject = waypoint.gameObject;
   }
}


       
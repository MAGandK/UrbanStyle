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
         selectedWaypoint._nextWaypoint._previosWaypoint = selectedWaypoint._previosWaypoint;
      }

      if (selectedWaypoint._previosWaypoint != null)
      {
         selectedWaypoint._previosWaypoint._nextWaypoint = selectedWaypoint._nextWaypoint;
         Selection.activeGameObject = selectedWaypoint._previosWaypoint.gameObject;
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

      newWypoint._previosWaypoint = selectedWaypoint;
      
      if(selectedWaypoint._nextWaypoint != null)
      {
         selectedWaypoint._nextWaypoint._previosWaypoint = newWypoint;
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

      if (selectedWaypoint._previosWaypoint != null)
      {
         newWaypoint._previosWaypoint = selectedWaypoint._previosWaypoint;
         selectedWaypoint._previosWaypoint._nextWaypoint = newWaypoint;
      }

      newWaypoint._nextWaypoint = selectedWaypoint;
      selectedWaypoint._previosWaypoint = newWaypoint;
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
         waypoint._previosWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
         waypoint._previosWaypoint._nextWaypoint = waypoint;

         waypoint.transform.position = waypoint._previosWaypoint.transform.position;
         waypoint.transform.forward = waypoint._previosWaypoint.transform.forward;
      }
      
      Selection.activeGameObject = waypoint.gameObject;
   }
}


       
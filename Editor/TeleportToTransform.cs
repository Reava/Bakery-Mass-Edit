using UnityEngine;
using UnityEditor;

namespace UwUtils
{
    public class TeleportSelectedObjectsWindow : EditorWindow
    {
        private Transform targetTransform;

        [MenuItem("Tools/Reava_/Teleport Selected To Target")]
        public static void ShowWindow()
        {
            GetWindow<TeleportSelectedObjectsWindow>("Teleport Objects");
        }

        private void OnGUI()
        {
            GUILayout.Label("Teleport Settings", EditorStyles.boldLabel);

            targetTransform = (Transform)EditorGUILayout.ObjectField("Target Transform", targetTransform, typeof(Transform), true);

            EditorGUILayout.Space(15);

            GUI.enabled = targetTransform != null && Selection.transforms.Length > 0;

            if (GUILayout.Button("Teleport Selected Objects"))
            {
                foreach (Transform selected in Selection.transforms)
                {
                    Undo.RecordObject(selected, "Teleport Object");
                    selected.position = targetTransform.position;
                    selected.rotation = targetTransform.rotation;
                }
            }

            GUI.enabled = true;
        }
    }
}
using UnityEditor;
using UnityEngine;

namespace UwUtils
{
    public class SequencialNaming
    {
        [MenuItem("Tools/Reava_/Rename Children Sequentially")]
        public static void RenameChildrenSequentially()
        {
            GameObject selected = Selection.activeGameObject;

            if (selected == null)
            {
                Debug.LogError("No GameObject selected.");
                return;
            }

            int childCount = selected.transform.childCount;
            if (childCount == 0)
            {
                Debug.LogWarning("Selected GameObject has no children.");
                return;
            }

            Transform[] children = new Transform[childCount];
            for (int i = 0; i < childCount; i++)
            {
                children[i] = selected.transform.GetChild(i);
            }

            // Try to get starting number from the first child's name
            if (!int.TryParse(children[0].name, out int startIndex))
            {
                Debug.LogError($"First child's name '{children[0].name}' could not be parsed as an integer. Set it to the number you want to start with!");
                return;
            }

            Undo.RecordObjects(children, "Rename Children");

            for (int i = 0; i < childCount; i++)
            {
                children[i].name = (startIndex + i).ToString();
            }

            Debug.Log($"Renamed {childCount} children starting from {startIndex}.");
        }
    }
}

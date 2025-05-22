using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace UwUtils
{
    public class ToggleVRCLV : MonoBehaviour
    {
        [MenuItem("Tools/Reava_/Toggle _VRCLV/Enable All In Project")]
        static void EnableVRCLV()
        {
            ToggleVRCLVKeyword(true);
        }

        [MenuItem("Tools/Reava_/Toggle _VRCLV/Disable All In Project")]
        static void DisableVRCLV()
        {
            ToggleVRCLVKeyword(false);
        }

        [MenuItem("Tools/Reava_/Toggle _VRCLV/Enable All In Scene")]
        static void EnableVRCLV_Scene()
        {
            ToggleVRCLVKeyword_Scene(true);
        }

        [MenuItem("Tools/Reava_/Toggle _VRCLV/Disable All In Scene")]
        static void DisableVRCLV_Scene()
        {
            ToggleVRCLVKeyword_Scene(false);
        }

        static void ToggleVRCLVKeyword(bool enable)
        {
            string keyword = "_VRCLV";

            // Find all materials in the project
            string[] guids = AssetDatabase.FindAssets("t:Material");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

                if (mat != null && mat.HasProperty("_VRCLV"))
                {
                    mat.SetFloat("_VRCLV", enable ? 1 : 0);
                    if (enable)
                        mat.EnableKeyword(keyword);
                    else
                        mat.DisableKeyword(keyword);

                    EditorUtility.SetDirty(mat);
                }
            }

            AssetDatabase.SaveAssets();
            Debug.Log($"_VRCLV has been {(enable ? "enabled" : "disabled")} for all materials.");
        }
        static void ToggleVRCLVKeyword_Scene(bool enable)
        {
            string keyword = "_VRCLV";
            HashSet<Material> processedMaterials = new HashSet<Material>();

            foreach (Renderer renderer in Object.FindObjectsOfType<Renderer>())
            {
                foreach (Material mat in renderer.sharedMaterials)
                {
                    if (mat != null && !processedMaterials.Contains(mat))
                    {
                        processedMaterials.Add(mat);

                        if (mat.HasProperty("_VRCLV"))
                        {
                            mat.SetFloat("_VRCLV", enable ? 1 : 0);
                            if (enable)
                                mat.EnableKeyword(keyword);
                            else
                                mat.DisableKeyword(keyword);

                            EditorUtility.SetDirty(mat);
                        }
                    }
                }
            }

            AssetDatabase.SaveAssets();
            Debug.Log($"_VRCLV has been {(enable ? "enabled" : "disabled")} for all scene materials.");
        }
    }
}
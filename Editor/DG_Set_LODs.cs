using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
namespace DG_Toolchain
{
    public class DG_Set_LODs : EditorWindow
    {
        static public void OnGUI()
        {
            EditorGUILayout.HelpBox("Select direct parents of the _LOD children. You may want to change transition distances after.", MessageType.Info);
            EditorGUI.BeginDisabledGroup(Selection.gameObjects.Length == 0);
            if (GUILayout.Button("Set LOD groups"))
            {
                SetLODs();
            }
            EditorGUI.EndDisabledGroup();
        }

        static public void SetLODs()
        {
            GameObject[] selectedObjects = Selection.gameObjects;

            foreach (GameObject selectedObject in selectedObjects)
            {
                bool hasLODs = false;

                // Check if the object has children with LOD suffix
                foreach (Transform child in selectedObject.transform)
                {
                    for (int lodIndex = 0; lodIndex <= 5; lodIndex++) // Check for LOD0 to LOD5
                    {
                        string lodSuffix = "_LOD" + lodIndex;
                        if ((child.gameObject.GetComponent<Renderer>() != null) && child.name.EndsWith(lodSuffix))//Add additional check if _LODx is not a mesh
                        {
                            hasLODs = true;
                            break;
                        }

                    }

                    if (hasLODs)
                    {
                        break;
                    }
                }

                if (hasLODs)
                {
                    Undo.RecordObject(selectedObject, "Set LODs");

                    // Check if the object has a LODGroup component; if not, add one
                    LODGroup lodGroup = selectedObject.GetComponent<LODGroup>();
                    if (lodGroup == null)
                    {
                        lodGroup = selectedObject.AddComponent<LODGroup>();
                    }

                    // Iterate through children and add LODs
                    List<LOD> lods = new List<LOD>();
                    foreach (Transform child in selectedObject.transform)
                    {
                        for (int lodIndex = 0; lodIndex <= 5; lodIndex++) // Check for LOD0 to LOD5
                        {
                            string lodSuffix = "_LOD" + lodIndex;
                            if (child.name.EndsWith(lodSuffix))
                            {
                                MeshRenderer meshRenderer = child.GetComponent<MeshRenderer>();
                                SkinnedMeshRenderer skinnedMeshRenderer = child.GetComponent<SkinnedMeshRenderer>();
                                // Add mesh renderers or skinned mesh renderers to LODGroup
                                if (meshRenderer != null || skinnedMeshRenderer != null)
                                {
                                    Renderer[] renderers = child.GetComponentsInChildren<Renderer>();
                                    LOD lod = new LOD(1.0F / (lodIndex + 3), renderers);
                                    lods.Add(lod);
                                }
                            }
                        }
                    }

                    // Set the LODs for the LODGroup
                    lodGroup.SetLODs(lods.ToArray());
                }
            }
        }
    }
}
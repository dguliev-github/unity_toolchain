using UnityEditor;
using UnityEngine;

namespace DG_Toolchain
{
    public class DG_SwapMaterial : EditorWindow
    {
        static private Material removeMaterial;
        static private Material replaceMaterial;

        static public void OnGUI()
        {
            EditorGUILayout.HelpBox("Swaps first material with the second one in all selected mesh renderers. Good for making prefab variations.", MessageType.Info);

            removeMaterial = EditorGUILayout.ObjectField("Material to remove:", removeMaterial, typeof(Material), false) as Material;
            replaceMaterial = EditorGUILayout.ObjectField("Material to replace with:", replaceMaterial, typeof(Material), false) as Material;


            EditorGUI.BeginDisabledGroup(removeMaterial == null || replaceMaterial == null);
            if (GUILayout.Button("Swap Materials In Selected"))
            {
                SwapMaterials();
            }
            EditorGUI.EndDisabledGroup();
        }

        static private void SwapMaterials()
        {
            GameObject[] selection = Selection.gameObjects;

            foreach (GameObject obj in selection)
            {
                MeshRenderer[] renderers = obj.GetComponentsInChildren<MeshRenderer>();

                Undo.RecordObject(obj, "Swap Materials");

                foreach (MeshRenderer renderer in renderers)
                {
                    SerializedObject serializedRenderer = new SerializedObject(renderer);
                    SerializedProperty materialsProperty = serializedRenderer.FindProperty("m_Materials");

                    for (int i = 0; i < materialsProperty.arraySize; i++)
                    {
                        if (materialsProperty.GetArrayElementAtIndex(i).objectReferenceValue == removeMaterial)
                        {
                            materialsProperty.GetArrayElementAtIndex(i).objectReferenceValue = replaceMaterial;
                        }
                    }

                    serializedRenderer.ApplyModifiedProperties();
                }
            }
        }
    }
}
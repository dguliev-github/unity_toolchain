using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DG_Toolchain
{
    public class DG_SelectWithSameMat : EditorWindow
    {
        static private Material selectedMat;

        static public void OnGUI()
        {
            // Get the currently selected material
            selectedMat = Selection.activeObject as Material;
            EditorGUILayout.HelpBox("Choose material to be found from the Project window.", MessageType.Info);
            EditorGUI.BeginDisabledGroup(selectedMat == null);
            if (GUILayout.Button("Select with the same material"))
            {
                SelectMeshesWithSameMaterial();
            }
            EditorGUI.EndDisabledGroup();
        }

        static private void SelectMeshesWithSameMaterial()
        {

            // Collect all mesh renderers with the same material
            List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
            foreach (MeshRenderer mr in FindObjectsOfType<MeshRenderer>())
            {
                if (mr.sharedMaterial == selectedMat)
                {
                    meshRenderers.Add(mr);
                }
            }

            // Select all the mesh gameobjects with the same material
            List<GameObject> selectedObjects = new List<GameObject>();
            foreach (MeshRenderer mr in meshRenderers)
            {
                selectedObjects.Add(mr.gameObject);
            }
            Selection.objects = selectedObjects.ToArray();
        }

    }
}
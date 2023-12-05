using UnityEngine;
using UnityEditor;
using System.IO;

public class DG_GeneratePrefabs : EditorWindow
{
    static private string saveFolder = "Assets/_Original/Prefabs/";
    static private GameObject selectedObject;

    static public void OnGUI()
    {   

        EditorGUILayout.HelpBox("Generate Prefabs from Selected Object's Children Meshes. Good for preparing prefab kits from single fbx file.", MessageType.Info);
        selectedObject = Selection.activeGameObject;
        saveFolder = EditorGUILayout.TextField("Save Folder", saveFolder);

        EditorGUI.BeginDisabledGroup(selectedObject == null);
        if (GUILayout.Button("Generate"))
            GeneratePrefabs();
        EditorGUI.EndDisabledGroup();
    }

    static private void GeneratePrefabs()
    {
        
        MeshFilter[] meshFilters = selectedObject.GetComponentsInChildren<MeshFilter>(true);

        foreach (MeshFilter meshFilter in meshFilters)
        {
            GameObject parentPrefab = new GameObject(meshFilter.sharedMesh.name);
            parentPrefab.AddComponent<MeshFilter>().sharedMesh = meshFilter.sharedMesh;
            parentPrefab.AddComponent<MeshRenderer>().sharedMaterial = meshFilter.GetComponent<MeshRenderer>().sharedMaterial;

            string prefabPath = saveFolder + meshFilter.sharedMesh.name + ".prefab";
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            PrefabUtility.SaveAsPrefabAsset(parentPrefab, prefabPath);
            DestroyImmediate(parentPrefab);
        }
    }
}
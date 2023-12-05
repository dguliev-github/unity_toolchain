using UnityEngine;
using UnityEditor;

public class DGs_Toolchain : EditorWindow
{
    private int selectedToolIndex = 2;
    private string[] toolOptions = { "Generate Prefabs", "Swap Material", "Replace with Prefab", "Select with the same Material", "Copy Transforms", "Select Children" }; //append with yourEditorScriptName

    [MenuItem("Tools/DG's Toolchain")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DGs_Toolchain));
    }

    private void OnGUI()
    {
        selectedToolIndex = EditorGUILayout.Popup("Select Tool:", selectedToolIndex, toolOptions);
        switch (selectedToolIndex)
        {
            case 0:
                DG_GeneratePrefabs.OnGUI();
                break;
            case 1:
                DG_SwapMaterial.OnGUI();
                break;
            case 2:
                DG_ReplaceWithPrefab.OnGUI();
                break;
            case 3:
                DG_SelectWithSameMat.OnGUI();
                break;
            case 4:
                DG_CopyTransforms.OnGUI();
                break;
            case 5:
                DG_Select_Children.OnGUI();
                break;
          //case n:
          //    yourEditorScript.OnGUI();
          //    break;
        }
    }
}

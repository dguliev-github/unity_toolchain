using UnityEngine;
using UnityEditor;

public class DGs_Toolchain : EditorWindow
{
    private int selectedToolIndex = 0;
    private string[] toolOptions = { "Generate Prefabs", "Swap Materials", "Replace with Prefab", "Select with the same Material", "Copy Transforms", "Select Children" }; //append with yourEditorScriptName
    private DG_GeneratePrefabs generatePrefabs;
    private DG_SwapMaterial swapMaterial;
    private DG_ReplaceWithPrefab replaceWithPrefab;
    private DG_SelectWithSameMat selectWithSameMat;
    private DG_CopyTransforms copyTransforms;
    private DG_Select_Children selectChildren;
  //private YourEditorScript yourEditorScriptInstance;

    [MenuItem("Tools/DG's Toolchain")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DGs_Toolchain));
    }

    private void OnEnable()
    {
        generatePrefabs = CreateInstance<DG_GeneratePrefabs>();
        swapMaterial = CreateInstance<DG_SwapMaterial>();
        replaceWithPrefab = CreateInstance<DG_ReplaceWithPrefab>();
        selectWithSameMat = CreateInstance<DG_SelectWithSameMat>();
        copyTransforms = CreateInstance<DG_CopyTransforms>();
        selectChildren = CreateInstance<DG_Select_Children>();
      //yourEditorScriptInstance = CreateInstance<yourEditorScriptInstance>();
    }
    private void OnGUI()
    {
        selectedToolIndex = EditorGUILayout.Popup("Select Tool:", selectedToolIndex, toolOptions);
        switch (selectedToolIndex)
        {
            case 0:
                generatePrefabs.OnGUI();
                break;
            case 1:
                swapMaterial.OnGUI();
                break;
            case 2:
                replaceWithPrefab.OnGUI();
                break;
            case 3:
                selectWithSameMat.OnGUI();
                break;
            case 4:
                copyTransforms.OnGUI();
                break;
            case 5:
                selectChildren.OnGUI();
                break;
          //case n:
          //    yourEditorScriptInstance.OnGUI();
          //    break;
        }
    }
}

using UnityEngine;
using UnityEditor;

namespace DG_Toolchain
{
    public class DGs_Toolchain : EditorWindow
    {
        private int selectedToolIndex = 1;
        private string[] toolOptions = { "Swap Material", "Replace with Prefab", "Select with the same Material", "Copy Transforms", "Select Children", "Transfer Colliders", "Set LOD Groups", "Set LOD Transitions" }; //append with yourEditorScriptName

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
                    DG_SwapMaterial.OnGUI();
                    break;
                case 1:
                    DG_ReplaceWithPrefab.OnGUI();
                    break;
                case 2:
                    DG_SelectWithSameMat.OnGUI();
                    break;
                case 3:
                    DG_CopyTransforms.OnGUI();
                    break;
                case 4:
                    DG_Select_Children.OnGUI();
                    break;
                case 5:
                    DG_Transfer_Colliders.OnGUI();
                    break;
                case 6:
                    DG_Set_LODs.OnGUI();
                    break;
                case 7:
                    DG_Set_LOD_transitions.OnGUI();
                    break;
                //case n:
                //    yourEditorScript.OnGUI();
                //    break;
            }
        }
    }
}
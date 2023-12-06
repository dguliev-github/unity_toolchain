//https://gist.github.com/unity3dcollege/c1efea3f87d3775bee3e010e9c6d7648
//https://forum.unity.com/threads/replace-game-object-with-prefab.24311/

using UnityEngine;
using UnityEditor;

namespace DG_Toolchain
{
    public class DG_ReplaceWithPrefab : EditorWindow
    {
        static private GameObject[] selection;
        static private GameObject prefab;

        static public void OnGUI()
        {
            selection = Selection.gameObjects;
            prefab = (GameObject)EditorGUILayout.ObjectField("Prefab to replace with:", prefab, typeof(GameObject), true);
            EditorGUI.BeginDisabledGroup(selection.Length == 0 || prefab == null);
            if (GUILayout.Button("Replace Selected"))
            {
                ReplaceSelected();
            }
            EditorGUI.EndDisabledGroup();
        }

        static private void ReplaceSelected()
        {

            for (var i = selection.Length - 1; i >= 0; --i)
            {
                GameObject selected = selection[i];
                PrefabAssetType prefabType = PrefabUtility.GetPrefabAssetType(prefab);
                GameObject newObject;
                if (prefabType == PrefabAssetType.Regular || prefabType == PrefabAssetType.Variant)
                {
                    newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                }
                else
                {
                    newObject = Instantiate(prefab);
                    newObject.name = prefab.name;
                }
                if (newObject == null)
                {
                    Debug.LogError("Error instantiating prefab");
                    break;
                }
                Undo.RegisterCreatedObjectUndo(newObject, "Replace With Prefabs");
                newObject.transform.parent = selected.transform.parent;
                newObject.transform.localPosition = selected.transform.localPosition;
                newObject.transform.localRotation = selected.transform.localRotation;
                newObject.transform.localScale = selected.transform.localScale;
                newObject.transform.SetSiblingIndex(selected.transform.GetSiblingIndex());
                Undo.DestroyObjectImmediate(selected);
            }
        }
    }
}
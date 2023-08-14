using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class DG_Select_Children : EditorWindow
{
    private bool selectAllChildren = false;
    private GameObject[] selectedObjects;
    //[MenuItem("Tools/DG_Select_Children")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DG_Select_Children));
    }

    public void OnGUI()
    {
        EditorGUIUtility.labelWidth = 200;
        EditorGUILayout.HelpBox("Selects children of all selected parent objects", MessageType.Info);
        selectAllChildren = EditorGUILayout.Toggle(new GUIContent("Select All Children Recursively"), selectAllChildren, GUILayout.ExpandWidth(true));
        selectedObjects = Selection.gameObjects;
        EditorGUI.BeginDisabledGroup(selectedObjects.Length == 0);
        if (GUILayout.Button("Select Children"))
        {
            SelectChildren();
        }
        EditorGUI.EndDisabledGroup();
    }

    private void SelectChildren()
    {
        

        Undo.RecordObjects(selectedObjects, "Select Children");

        List<GameObject> allChildren = new List<GameObject>();

        foreach (GameObject obj in selectedObjects)
        {
            if (selectAllChildren)
            {
                CollectChildrenRecursively(obj.transform, allChildren);
            }
            else
            {
                CollectDirectChildren(obj.transform, allChildren);
            }
        }

        Selection.objects = allChildren.ToArray();
    }

    private void CollectDirectChildren(Transform parent, List<GameObject> childrenList)
    {
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            childrenList.Add(parent.GetChild(i).gameObject);
        }
    }

    private void CollectChildrenRecursively(Transform parent, List<GameObject> childrenList)
    {
        CollectDirectChildren(parent, childrenList);

        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Transform child = parent.GetChild(i);
            if (child.childCount > 0)
            {
                CollectChildrenRecursively(child, childrenList);
            }
        }
    }
}
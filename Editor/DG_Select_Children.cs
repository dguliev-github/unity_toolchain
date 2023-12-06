using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace DG_Toolchain
{
    public class DG_Select_Children : EditorWindow
    {
        static private bool selectAllChildren = false;
        static private GameObject[] selectedObjects;

        static public void OnGUI()
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

        static private void SelectChildren()
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

        static private void CollectDirectChildren(Transform parent, List<GameObject> childrenList)
        {
            int childCount = parent.childCount;
            for (int i = 0; i < childCount; i++)
            {
                childrenList.Add(parent.GetChild(i).gameObject);
            }
        }

        static private void CollectChildrenRecursively(Transform parent, List<GameObject> childrenList)
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
}
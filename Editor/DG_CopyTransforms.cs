using UnityEditor;
using UnityEngine;

public class DG_CopyTransforms : EditorWindow
{
    private GameObject donorObject;
    private GameObject targetObject;

    //[MenuItem("Tools/DG_CopyTransforms")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(DG_CopyTransforms));
    }

    public void OnGUI()
    {
        EditorGUILayout.HelpBox("Recusively copies transforms including children with same names.", MessageType.Info);
        donorObject = (GameObject)EditorGUILayout.ObjectField("Donor Object", donorObject, typeof(GameObject), true);
        targetObject = (GameObject)EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true);

        
        EditorGUI.BeginDisabledGroup(donorObject == null || targetObject == null);
        if (GUILayout.Button("Copy Transforms"))
        {
            CopyTransforms();
        }
        EditorGUI.EndDisabledGroup();
    }

    private void CopyTransforms()
    {

        Undo.RecordObject(targetObject.transform, "Copy Transforms");

        Transform donorTransform = donorObject.transform;
        Transform targetTransform = targetObject.transform;

        targetTransform.position = donorTransform.position;
        targetTransform.rotation = donorTransform.rotation;
        targetTransform.localScale = donorTransform.localScale;

        CopyChildTransforms(donorTransform, targetTransform);
    }

    private void CopyChildTransforms(Transform donorParent, Transform targetParent)
    {
        for (int i = 0; i < donorParent.childCount; i++)
        {
            Transform donorChild = donorParent.GetChild(i);
            Transform targetChild = targetParent.Find(donorChild.name);

            if (targetChild != null)
            {
                Undo.RecordObject(targetChild, "Copy Transforms");
                targetChild.position = donorChild.position;
                targetChild.rotation = donorChild.rotation;
                targetChild.localScale = donorChild.localScale;

                CopyChildTransforms(donorChild, targetChild);
            }
        }
    }
}

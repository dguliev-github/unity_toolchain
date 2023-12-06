using UnityEditor;
using UnityEngine;

namespace DG_Toolchain
{
    public class DG_Transfer_Colliders : EditorWindow
    {
        static private string childName = "Empty";

        static public void OnGUI()
        {
            EditorGUILayout.HelpBox("If there is no child with the provided name new one will be created instead.", MessageType.Info);
            childName = EditorGUILayout.TextField("Child Name:", childName);
            EditorGUI.BeginDisabledGroup(Selection.activeGameObject == null || childName == string.Empty);
            if (GUILayout.Button("Transfer Colliders To Child"))
            {
                TransferColliders();
            }
            EditorGUI.EndDisabledGroup();
        }

        static private void TransferColliders()
        {

            Transform parentTransform = Selection.activeGameObject.transform;
            Transform childTransform = FindOrCreateChild(parentTransform, childName);

            if (childTransform != null)
            {
                Collider[] colliders = parentTransform.GetComponents<Collider>();

                foreach (Collider collider in colliders)
                {
                    if (collider != null)
                    {
                        Undo.RecordObject(collider, "Transfer Collider");
                        Collider newCollider = Undo.AddComponent(childTransform.gameObject, collider.GetType()) as Collider;
                        EditorUtility.CopySerialized(collider, newCollider);
                        Undo.DestroyObjectImmediate(collider);
                    }
                }
            }
        }

        static private Transform FindOrCreateChild(Transform parent, string childName)
        {
            Transform child = parent.Find(childName);

            if (child == null)
            {
                GameObject newChild = new GameObject(childName);
                newChild.transform.SetParent(parent);
                newChild.transform.localPosition = Vector3.zero;
                newChild.transform.localRotation = Quaternion.identity;
                newChild.transform.localScale = Vector3.one;

                return newChild.transform;
            }

            return child;
        }
    }
}
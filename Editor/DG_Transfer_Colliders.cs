using UnityEditor;
using UnityEngine;

namespace DG_Toolchain
{
    public class DG_Transfer_Colliders : EditorWindow
    {
        
        static public void OnGUI()
        {
            GameObject selected = Selection.activeGameObject;
            GUILayout.BeginHorizontal();
            EditorGUI.BeginDisabledGroup((selected == null) || (selected.transform.parent == null) || (selected.transform.parent.GetComponent<Collider>() == null));
            if (GUILayout.Button("To Selected Child", UnityEditor.EditorStyles.miniButtonLeft)) 
            {
                TransferCollidersToChild();
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup((selected == null) || !HasComponentInChildren<Collider>(selected));
            if (GUILayout.Button("To Selected Parent", UnityEditor.EditorStyles.miniButtonRight))
            {
                TransferCollidersToParent();
            }
            EditorGUI.EndDisabledGroup();
            GUILayout.EndHorizontal();     
        }

        static bool HasComponentInChildren<T>(GameObject obj) where T : Component
        {
            bool result = false;
            foreach (Transform childTransform in obj.transform)
            {
                if (childTransform.GetComponent<T>() != null)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        static private void TransferCollidersToParent()
        {

            Collider[] childColliders = Selection.activeGameObject.GetComponentsInChildren<Collider>(true);

            foreach (Collider childCollider in childColliders)
            {
                Undo.RecordObject(childCollider, "Transfer Collider to Parent");
                Collider newCollider = Undo.AddComponent(Selection.activeGameObject.gameObject, childCollider.GetType()) as Collider;
                EditorUtility.CopySerialized(childCollider, newCollider);
                Undo.DestroyObjectImmediate(childCollider);
            }
        }

        static private void TransferCollidersToChild()
        {

            Transform childTransform = Selection.activeGameObject.transform;
            Transform parentTransform = Selection.activeGameObject.transform.parent;

                Collider[] colliders = parentTransform.GetComponents<Collider>();

                foreach (Collider collider in colliders)
                {
                    if (collider != null)
                    {
                        Undo.RecordObject(collider, "Transfer Collider to Child");
                        Collider newCollider = Undo.AddComponent(childTransform.gameObject, collider.GetType()) as Collider;
                        EditorUtility.CopySerialized(collider, newCollider);
                        Undo.DestroyObjectImmediate(collider);
                    }
                }
        }
    }
}
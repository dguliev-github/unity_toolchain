using UnityEditor;
using UnityEngine;

public class DG_Set_LOD_transitions : EditorWindow
{

    static SerializedObject serializedObject;

    static  float firstLODTransitionPercent = 50f;
    static float lastLODTransitionPercent = 1f;

    static public void OnGUI()
    {
        EditorGUILayout.HelpBox("Linear interpoilate transition percents between first and last LOD. Useful when selected objects don't have the same LOD count.", MessageType.Info);
        GUILayout.BeginHorizontal();
        firstLODTransitionPercent = EditorGUILayout.FloatField("First LOD Transition %", firstLODTransitionPercent);
        lastLODTransitionPercent = EditorGUILayout.FloatField("Last LOD Transition %", lastLODTransitionPercent);
        GUILayout.EndHorizontal();
        EditorGUI.BeginDisabledGroup((Selection.gameObjects.Length == 0) || (firstLODTransitionPercent <= lastLODTransitionPercent) || !HasLODGroups());
        if (GUILayout.Button("Set LOD Transitions"))
        {
            SetLODTransitions();
        }
        EditorGUI.EndDisabledGroup();
    }

    static bool HasLODGroups()
    {   
        bool hasLODGroups = true;
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            if (selectedObject.GetComponent<LODGroup>() == null)
            {
               hasLODGroups = false;
            }
        }
        return hasLODGroups;
    }

    static void SetLODTransitions()
    {
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            SetLODTransitionsForObject(selectedObject);
        }
    }

    static void SetLODTransitionsForObject(GameObject obj)
    {
        LODGroup lodGroup = obj.GetComponent<LODGroup>();

        if (lodGroup != null)
        {
            serializedObject = new SerializedObject(lodGroup);

            Undo.RecordObject(lodGroup, "Set LOD Transitions");
            LOD[] lods = lodGroup.GetLODs();
            if (lodGroup.lodCount == 1)
                {
                    lods[0].screenRelativeTransitionHeight = lastLODTransitionPercent/100f;
                    lodGroup.SetLODs(lods);
            }
            else
            {
                for (int i = 0; i < lodGroup.lodCount; i++)
                {
                    lods[i].screenRelativeTransitionHeight = (Mathf.Lerp(firstLODTransitionPercent, lastLODTransitionPercent, (float)i / (lodGroup.lodCount - 1))) / 100f;
                }
                lodGroup.SetLODs(lods);
            }
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            Debug.LogWarning("Selected object does not have LODGroup component.");
        }
    }
}

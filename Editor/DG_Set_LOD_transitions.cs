using System;
using UnityEditor;
using UnityEngine;

public class DG_Set_LOD_transitions : EditorWindow
{

    static SerializedObject serializedObject;

    static float firstLODTransitionPercent = 50f;
    static float lastLODTransitionPercent = 1f;
    static LODFadeMode fadeMode = LODFadeMode.CrossFade;
    static bool fadeAnim;
    static float crossFadeAnimationDuration = 5f;
    static float LODfadeTransitionWidthPercent = 10f;

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
        GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Fade Mode", GUILayout.MaxWidth(75f)); fadeMode = (LODFadeMode)EditorGUILayout.EnumPopup(fadeMode,GUILayout.MaxWidth(100f));

            GUILayout.FlexibleSpace();

            EditorGUI.BeginDisabledGroup(fadeMode != LODFadeMode.CrossFade);
                EditorGUILayout.LabelField("  Animate", GUILayout.MaxWidth(65f)); fadeAnim = EditorGUILayout.Toggle(fadeAnim, GUILayout.MaxWidth(20f));
            EditorGUI.EndDisabledGroup();

            GUILayout.FlexibleSpace();

            EditorGUI.BeginDisabledGroup(fadeMode != LODFadeMode.CrossFade);
                if (fadeAnim == true)
                    {
                        EditorGUILayout.LabelField("(Global) Fade Animation Time", GUILayout.MaxWidth(180f));
                        crossFadeAnimationDuration = EditorGUILayout.FloatField(crossFadeAnimationDuration);
                    }
                else
                    {
                        EditorGUILayout.LabelField("Fade Transition Width %", GUILayout.MaxWidth(140f));
                        LODfadeTransitionWidthPercent = EditorGUILayout.FloatField(LODfadeTransitionWidthPercent);
                    }         
            EditorGUI.EndDisabledGroup();

        GUILayout.EndHorizontal();
        if (GUILayout.Button("Set LOD Fade"))
        {
            SetLODCrossfade();
        }
        GUILayout.FlexibleSpace();
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

    static void SetLODCrossfade()
    {
        foreach (GameObject selectedObject in Selection.gameObjects)
        {
            SetLODCrossfadeForObject(selectedObject);
        }
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
                lods[0].screenRelativeTransitionHeight = lastLODTransitionPercent / 100f;

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

    static void SetLODCrossfadeForObject(GameObject obj)
    {
        LODGroup lodGroup = obj.GetComponent<LODGroup>();

        if (lodGroup != null)
        {
            serializedObject = new SerializedObject(lodGroup);

            Undo.RecordObject(lodGroup, "Set LOD CrossFade");
            LOD[] lods = lodGroup.GetLODs();
            LODGroup.crossFadeAnimationDuration = crossFadeAnimationDuration;
            lodGroup.animateCrossFading = fadeAnim;
            lodGroup.fadeMode = fadeMode;
            for (int i = 0; i < lodGroup.lodCount; i++)
            {
                lods[i].fadeTransitionWidth = LODfadeTransitionWidthPercent / 100f;
            }
            lodGroup.SetLODs(lods);
            serializedObject.ApplyModifiedProperties();
        }
        else
        {
            Debug.LogWarning("Selected object does not have LODGroup component.");
        }
    }
}

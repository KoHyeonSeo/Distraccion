using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(MovingGround))]
public class MovingEditor : Editor
{
    private MovingGround Generator;
    SerializedProperty dataset;
    private void OnEnable()
    {
        Generator = target as MovingGround;
        dataset = serializedObject.FindProperty("dataSets");
    }

    private void OnSceneGUI()
    {
        if (Generator.dataSets.Count <= 1)
        {
            return;
        }

        EditorGUI.BeginChangeCheck();
        Queue<Vector3> buffer = new Queue<Vector3>();
        for (int i = 0; i < Generator.dataSets.Count; i++)
        {
            buffer.Enqueue(Handles.PositionHandle(Generator.dataSets[i], Quaternion.identity));
        }

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(Generator, $"Change {nameof(Generator.dataSets)}");

            for (int i = 0; i < Generator.dataSets.Count; i++)
            {
                Generator.dataSets[i] = buffer.Dequeue();
            }
            EditorUtility.SetDirty(Generator);
        }

        for (int i = 0; i < Generator.dataSets.Count - 1; i++)
        {
            Handles.DrawLine(Generator.dataSets[i], Generator.dataSets[i + 1]);
        }
        int detail = 50;
        for (float i = 0; i < detail; i++)
        {
            float value_before = i / detail;
            Vector3 before = PhysicsUtility.BezierCurve(Generator.dataSets, value_before);

            float value_after = (i + 1) / detail;
            Vector3 after = PhysicsUtility.BezierCurve(Generator.dataSets, value_after); ;


            Handles.DrawLine(before, after);
        }
    }
}

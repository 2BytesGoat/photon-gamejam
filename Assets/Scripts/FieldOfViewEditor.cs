using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (EnemyAI))]
public class FieldOfViewEditor : Editor {
    void OnSceneGUI() {
        EnemyAI enemyAI = (EnemyAI)target;
        Handles.color = Color.red;
        Handles.DrawWireDisc(enemyAI.transform.position, new Vector3(0f, 0f, 1f), enemyAI.detectionRadiusSize);
    }
}

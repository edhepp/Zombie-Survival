using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ZombieStateBehaviour))]
public class ZombieStateBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        ZombieStateBehaviour zombieStateBehaviour = (ZombieStateBehaviour)target;
        if (GUILayout.Button("Interact"))
        {
            zombieStateBehaviour.Interact();
        }

        if (GUILayout.Button("Take Damage"))
        {
            zombieStateBehaviour.TakeDamage();
        }

        if (GUILayout.Button("Attack"))
        {
            zombieStateBehaviour.Attack();
        }
    }
    
}

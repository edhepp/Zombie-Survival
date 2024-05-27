using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BarrierStateBehaviour))]
public class BarrierBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BarrierStateBehaviour barrierStateBehaviour = (BarrierStateBehaviour)target;
        if (GUILayout.Button("Interact"))
        {
            barrierStateBehaviour.Interact();
        }
        
        if (GUILayout.Button("Take Default Damage"))
        {
            barrierStateBehaviour.TakeDamage();
        }

        if (GUILayout.Button("Repair Default Mult"))
        {
            barrierStateBehaviour.Repair();
        }

    }
}
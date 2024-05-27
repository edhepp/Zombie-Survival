using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BarrierBehaviour))]
public class BarrierBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        BarrierBehaviour barrierBehaviour = (BarrierBehaviour)target;
        if (GUILayout.Button("Interact"))
        {
            barrierBehaviour.Interact();
        }
        
        if (GUILayout.Button("Take Default Damage"))
        {
            barrierBehaviour.TakeDamage();
        }

        if (GUILayout.Button("Repair Default Mult"))
        {
            barrierBehaviour.Repair();
        }

    }
}
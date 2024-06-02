using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GameManager))]
public class GameManagerStateBehaviourEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameManager gameManager = (GameManager)target;
        if (GUILayout.Button("Main Menu"))
        {
            gameManager.MainMenu();
        }

        if (GUILayout.Button("Game Started"))
        {
            gameManager.InGame();
        }

        if (GUILayout.Button("Game Over"))
        {
            gameManager.GameOver();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveDataManager))]
public class SavaManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {




        DrawDefaultInspector();
        SaveDataManager data = (SaveDataManager)target;
        if (GUILayout.Button("Save Data"))
        {
            data.SaveAllData();
        }

        if (GUILayout.Button("Load Data"))
        {

            data.LoadAllData();
        }


        if (GUILayout.Button("Delete Data"))
        {

            data.DeleteSaveData();
        }


    }
}

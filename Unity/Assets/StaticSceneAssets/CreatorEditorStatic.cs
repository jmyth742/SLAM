//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;


//[CustomEditor(typeof(CreatorScriptStatic))]
//public class CreatorEditorStatic : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        CreatorScriptStatic script = (CreatorScriptStatic)target;

//        if (GUILayout.Button("Read Points"))
//        {
//            string filename = EditorUtility.OpenFilePanel("Open PCD File", "", "");
//            script.readPCDFile(filename);
//        }

//        if (GUILayout.Button("Show Cloud"))
//        {

//            script.drawPCD();

//        }
//    }


//}

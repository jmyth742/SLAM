//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(CreatorDynamicScript))]
//public class CreatorDynamicEditor : Editor
//{

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        CreatorDynamicScript script = (CreatorDynamicScript)target;

//        if (GUILayout.Button("Read Points"))
//        {
//            string filename = EditorUtility.OpenFilePanel("Open PCD File", "", ".pcd");
//            script.readPCDFile(filename);
//        }

//        if (GUILayout.Button("Show Cloud"))
//        {

//            script.drawPCDOld();

//        }
//    }


//}

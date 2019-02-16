//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(Miro3DCamera))]
//public class Miro3DCameraEditor : Editor {

//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();
        
//        Miro3DCamera script = (Miro3DCamera)target;

//        //if (GUILayout.Button("Draw Extrema"))
//        //{
//        //    script.DrawExtrema(true);
//        //}

//        //if (GUILayout.Button("Clear Extrema"))
//        //{
//        //    script.DrawExtrema(false);
//        //}

//        if (GUILayout.Button("Capture Points"))
//        {
//            Vector3[] pointCloud;
//            script.CalcPointCloud(out pointCloud);
//            //script.DrawLastCapturedPoints(true);
//        }

//        if (GUILayout.Toggle(true, "Add color"))
//        {
            
//        }

//        if (GUILayout.Toggle(true, "Add noise"))
//        {

//        }

//        //if (GUILayout.Button("Clear Last Captured Points"))
//        //{
//        //    script.DrawLastCapturedPoints(false);
//        //}

//        //if (GUILayout.Button("Save Last Captured Points"))
//        //{

//        //    string filename = EditorUtility.SaveFilePanel("Save pointcloud as PCD", "", "pointcloud.pcd", "pcd");
//        //    if (filename != "")
//        //    {
//        //        script.CreatePCD(filename);
//        //        Debug.Log("Pointcloud saved as " + filename);
//        //    } else
//        //    {
//        //        Debug.LogWarning("Empty filename string, cannot save the pointcloud.");
//        //    }

//        //}
//    }

    
//}

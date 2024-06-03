using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MapGen))]
public class MapGenEditor : Editor
{
   public override void OnInspectorGUI()
   {
      MapGen mapgen = (MapGen)target;

      if (DrawDefaultInspector()) {
         if (mapgen.autoUpdate) {
            mapgen.DrawMapInEditor();
         }
      }

      if (GUILayout.Button("generate")) {
         mapgen.DrawMapInEditor();
      }

   }
}

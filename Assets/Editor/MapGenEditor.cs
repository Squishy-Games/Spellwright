using System.Collections;
using System.Collections.Generic;
using StaticsStuff.scripts;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(MapGen))]
public class MapGenEditor : Editor
{
   public override void OnInspectorGUI()
   {
      MapGen mapgen = (MapGen)target;

      if (DrawDefaultInspector())
      {
         if (mapgen.autoUpdate)
         {
            mapgen.GenerateMap();
         }
      }

      if (GUILayout.Button("generate"))
      {
         mapgen.GenerateMap();
      }

   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Editor script for map generation, modified from:
// https://www.youtube.com/watch?v=WP-Bm65Q-1Y&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3&index=3&ab_channel=SebastianLague
[CustomEditor (typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target; 

        // When values change, automatically update
        if (DrawDefaultInspector())
        {
            if (mapGen.autoUpdate)
            {
                mapGen.GenerateMap(); 
            }
        }

        // Update when pressing generate
        if (GUILayout.Button("Generate"))
        {
            mapGen.GenerateMap();
        }
    }
}

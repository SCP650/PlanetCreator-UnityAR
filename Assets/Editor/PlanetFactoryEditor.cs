using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetFactory))]
public class PlanetFactoryEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create!"))
        {
            PlanetManager.instance.CreatePlanet(Vector3.zero,Quaternion.identity);
        }
    }

}

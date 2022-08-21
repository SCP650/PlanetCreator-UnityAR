using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetFactory))]
public class PlanetFactoryEditor : Editor
{
    private PlanetFactory factory;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create!"))
        {
            factory.CreatePlanet(Vector3.zero);
        }
    }

    private void OnEnable()
    {
        factory = (PlanetFactory)target;
    }
}

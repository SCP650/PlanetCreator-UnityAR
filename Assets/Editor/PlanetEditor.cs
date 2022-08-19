using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlanetGen))]
public class PlanetEditor : Editor
{
    PlanetGen planet;
    Editor shapeEditor;
    Editor colorEditor;

    public override void OnInspectorGUI()
    {
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            base.OnInspectorGUI();
            if (check.changed)
            {
                planet.GeneratePlanet();
            }
        }
        if(GUILayout.Button("Generate Planet"))
        {
            planet.GeneratePlanet();
        }
        DrawSettingsEditor(planet.shapeSettings, planet.OnShapeSettingsUpdated, ref shapeEditor);
        DrawSettingsEditor(planet.colorSettings, planet.OnColorSettingsUpdated, ref colorEditor);
    }

    void DrawSettingsEditor(Object settings, System.Action onSettingsUpdated, ref Editor editor)
    {
        if (settings != null)
        {
            EditorGUILayout.InspectorTitlebar(true, settings);
            using (var check = new EditorGUI.ChangeCheckScope())
            {

                CreateCachedEditor(settings, null, ref editor);
                editor.OnInspectorGUI();
                if (check.changed)
                {
                    if (onSettingsUpdated != null) onSettingsUpdated();
                }
            }
        }
        
    }

    private void OnEnable()
    {
        planet = (PlanetGen)target;
    }
}

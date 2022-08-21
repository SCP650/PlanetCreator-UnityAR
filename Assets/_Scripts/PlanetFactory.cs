using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFactory : MonoBehaviour
{
    public GameObject PlanetPrefab;

    public void CreatePlanet(Vector3 position)
    {
        float radius = Random.Range(0.5f, 5);
        ShapeSettings ss = ShapeSettings.CreateInstance(radius, CreateNoiseSettings());
        var gb = Instantiate(PlanetPrefab, position, Quaternion.identity);
        var pg = gb.GetComponent<PlanetGen>();
        pg.GeneratePlanet(ss);
    }

    private ShapeSettings.NoiseLayer[] CreateNoiseSettings()
    {
        ShapeSettings.NoiseLayer[] noiseLayer = new ShapeSettings.NoiseLayer[Random.Range(2,4)];
        for (int i = 0; i < noiseLayer.Length; i++)
        {
            var ns = new NoiseSettings();
            ns.simpleNoiseSettings = new NoiseSettings.SimpleNoiseSettings();
            ns.rigidNoiseSettings = new NoiseSettings.RigidNoiseSettings();

            ns.filterType = (i + 1 == noiseLayer.Length) ? NoiseSettings.FilterType.Rigid : NoiseSettings.FilterType.Simple;
            ns.simpleNoiseSettings.baseRoughness = (i == 0) ? 1 : Random.Range(0.5f,4);
            ns.simpleNoiseSettings.center = Random.insideUnitSphere * Random.Range(0.5f, 10);
            ns.simpleNoiseSettings.minValue = (i == 0) ? Random.Range(0.9f, 1.1f) : Random.Range(-3.0f, 3.0f);
            ns.simpleNoiseSettings.numLayer = 4;
            ns.simpleNoiseSettings.persistence = (i == 0) ? 0.5f : Random.Range(0.5f, 1.5f);
            ns.simpleNoiseSettings.roughtness = Random.Range(1.0f, 5f);
            ns.simpleNoiseSettings.strength = (i == 0) ? Random.Range(0.05f, 0.1f) : Random.Range(-3.0f, 3.0f);
            if (ns.filterType == NoiseSettings.FilterType.Rigid)
            {
                ns.rigidNoiseSettings.weightMultiplier = 2;
            }
            noiseLayer[i] = new ShapeSettings.NoiseLayer();
            noiseLayer[i].noiseSettings = ns;
            noiseLayer[i].enabled = true;
            noiseLayer[i].useFirstLayerAsMask = true;
        }
        return noiseLayer;
    }


    private void CreateColorSettings()
    {

    }
}

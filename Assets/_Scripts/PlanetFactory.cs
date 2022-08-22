using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFactory : MonoBehaviour
{
    public GameObject PlanetPrefab;
    public Shader shader;

    public Planet CreatePlanet(Vector3 position, Quaternion rotation)
    {
        float radius = Random.Range(0.1f, 2f);
        ShapeSettings ss = ShapeSettings.CreateInstance(radius, CreateNoiseSettings());

        Material mat = new Material(shader);
        mat.SetFloat("_smoothness", 0.5f);
        ColorSettings cs = ColorSettings.CreateInstance(CreateOceanGrident(), CreateOceanGrident(), mat);

        var gb = Instantiate(PlanetPrefab, position, rotation);
        var pg = gb.GetComponent<PlanetGen>();
        pg.GeneratePlanet(ss,cs);

        var planet = gb.GetComponent<Planet>();
        planet.Init(radius);
        return planet;
        
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
            ns.simpleNoiseSettings.persistence = (i == 0) ? 0.5f : Random.Range(0.3f, 1f);
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


    private Gradient CreateOceanGrident()
    {
        Gradient g = new Gradient();
        g.mode = GradientMode.Blend;
        GradientColorKey[] colorKeys = new GradientColorKey[Random.Range(2,6)];
        for (int i = 0; i < colorKeys.Length; i++)
        {
            float t = i / (colorKeys.Length - 1);
            Color c = new Color(Random.Range(0,1f), Random.Range(0, 1f), Random.Range(0, 1f),1);
            GradientColorKey k = new GradientColorKey(c,t);
            colorKeys[i] = k;
        }
        GradientAlphaKey[] alphakeys = new GradientAlphaKey[1];
        alphakeys[0] = new GradientAlphaKey(1,1);

        g.SetKeys(colorKeys, alphakeys);
        return g;
    }
}

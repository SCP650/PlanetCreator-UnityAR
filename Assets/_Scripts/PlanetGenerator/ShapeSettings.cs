using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ShapeSettings : ScriptableObject
{
    public float planetRadius = 1;
    public NoiseLayer[] noiseLayers;

    [System.Serializable]
    public class NoiseLayer
    {
        public bool enabled = true;
        public bool useFirstLayerAsMask = true;
        public NoiseSettings noiseSettings;
    }

    public void Init(float radius, NoiseLayer[] noiseLayers)
    {
        this.planetRadius = radius;
        this.noiseLayers = noiseLayers;
    }

    public static ShapeSettings CreateInstance(float radius, NoiseLayer[] noiseLayers)
    {
        var data = ScriptableObject.CreateInstance<ShapeSettings>();
        data.Init(radius, noiseLayers);
        return data;
    }
}

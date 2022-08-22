using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    NoiseSettings.RigidNoiseSettings settings;
    Noise noise = new Noise();

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0; //make it 0 to 1
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numLayer; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.center));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp(v*settings.weightMultiplier,0,1); //the higher regions (peaks) will have more details 

            noiseValue += v * amplitude;
            amplitude *= settings.persistence;
            frequency *= settings.roughtness;
        }
        noiseValue = noiseValue - settings.minValue;
        return noiseValue * settings.strength;
    }
}

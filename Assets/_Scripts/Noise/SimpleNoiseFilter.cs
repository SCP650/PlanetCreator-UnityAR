using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter : INoiseFilter 
{
    NoiseSettings.SimpleNoiseSettings settings;
    Noise noise = new Noise();

    public SimpleNoiseFilter(NoiseSettings.SimpleNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0; //make it 0 to 1
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        for (int i = 0; i < settings.numLayer; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.center);
            noiseValue += (v + 1) * 0.5f * amplitude;
            amplitude *= settings.persistence;
            frequency *= settings.roughtness;
        }
        noiseValue = noiseValue - settings.minValue;
        return noiseValue*settings.strength;
    }
}

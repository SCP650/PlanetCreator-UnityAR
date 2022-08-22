using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ColorSettings : ScriptableObject
{
    public Gradient gradient;
    public Gradient oceanColor;
    public Material planetMaterial;
    public void Init(Gradient landGradient, Gradient oceanColor, Material mat)
    {
        this.gradient = landGradient;
        this.oceanColor = oceanColor;
        planetMaterial = mat;
    }

    public static ColorSettings CreateInstance(Gradient landGradient, Gradient oceanColor, Material mat)
    {
        var data = ScriptableObject.CreateInstance<ColorSettings>();
        data.Init(landGradient, oceanColor, mat);
        return data;
    }
}

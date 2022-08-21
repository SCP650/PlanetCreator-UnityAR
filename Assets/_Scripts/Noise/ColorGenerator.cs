using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator 
{
    ColorSettings settings;
    Texture2D texture;
    const int textureResolution = 50;

    public void UpdateSettings(ColorSettings settings)
    {
        this.settings = settings;
        if(texture == null)
        {
            texture = new Texture2D(textureResolution * 2, 1);//first half are ocean texture
        }
        
    }

    public void UpdateElevation(MinMax elevationMinMax)
    {
        settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.Min, elevationMinMax.Max));
    }

    public void UpdateColor()
    {
        Color[] colors = new Color[textureResolution*2];
        for (int i = 0; i < textureResolution*2; i++)
        {
            if (i < textureResolution)
            {
                colors[i] = settings.oceanColor.Evaluate(i / (textureResolution - 1f));
            }
            else
            {
                colors[i] = settings.gradient.Evaluate((i - textureResolution)/ (textureResolution - 1f));
            }
            
        }
        texture.SetPixels(colors);
        texture.Apply();
        settings.planetMaterial.SetTexture("_texture", texture);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FalloffGen 
{
   public static float[,] GenerateFalloffMap(int size, AnimationCurve falloffCurve)
    {
        //Falloff curve ranges from 0 to 1 on the horizontal and vertical axis.
        float[,] map = new float[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float x = i / (float)size * 2 - 1;
                float y = j / (float)size * 2 - 1;

                float value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                
                //float value = -1 * ((falloffCurve.Evaluate(x) * falloffCurve.Evaluate(y) * 2) - 1);
                
                
                map[i, j] = Evaluate(value);
            }
        }
        
        return map;
    }
    
   
    static float Evaluate(float value)
    {
        float a = 3;
        float b = 2.2f;

        return Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value,a));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Juice
{
    public static float Shake(float origin, float intensity)
    {
        float output;

        output = origin + (Random.Range(-1f,1f)*intensity);

        return output;
    }
    public static Vector3 Shake(Vector3 origin, float intensity)
    {
        Vector3 output;
        
        output = origin + (Random.insideUnitSphere*intensity);

        return output;
    }
}

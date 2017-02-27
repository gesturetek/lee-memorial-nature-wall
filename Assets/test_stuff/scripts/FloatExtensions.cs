using UnityEngine;

public static class FloatExtensions  {

    public static bool Approximation(this float a, float b, float tolerance)
    {
        return (Mathf.Abs(a - b) < tolerance);
    }
}

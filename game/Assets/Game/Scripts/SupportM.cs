using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SupportM {


    public static int Max(int a, int b)
    {
        return a > b ? a : b;
    }

    public static int Min(int a, int b)
    {
        return a < b ? a : b;
    }

    //inclusive low and up
    public static int ToLimit(int number, int low, int high)
    {
        return Max(low, Min(high, number));
    }

    public static int bool2int(bool b)
    {
        return b ? 1 : 0;
    }

    public static float Sum(float[] arr)
    {
        float k=0;
        foreach (float temp in arr)
            k += temp;
        return k;
    }

}

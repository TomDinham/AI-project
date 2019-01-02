using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuzzyLogic
{

    public static int Grade(float value, float x0, float x1)
    {
        int result = 0;
        float x = value;
        if (x <= x0)
        {
            result = 0;
        }
        else if (x > x1)
        {
            result = 100;
        }
        else
        {
            result = Mathf.RoundToInt(((x / (x1 - x0)) - (x0 / (x1 - x0))) * 100);
            
        }
        return result;

    }
    public static int AND(int A, int B)
    {
        if (A > B)
        {
            return B;
        }
        else return A;

    }
    public static int OR(int A, int B)
    {
        if (A > B)
        {
            return A;
        }
        else return B;
    }
    public static int NOT(int A)
    {
        return 100 - A;
    }
    
}

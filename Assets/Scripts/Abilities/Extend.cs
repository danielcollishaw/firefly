#region Copyright
/*---------------------------------------------------------------*/
/*        File: Extend.cs                                        */
/*        Firefly                                                */
/*        AI For Game Programming - CAP 4053                     */
/*        Copyright (c) 2023 Serenity Studios                    */
/*        All rights reserved.                                   */
/*        Made with love, by Justin Sasso.                       */
/*---------------------------------------------------------------*/
#endregion

using System;
using UnityEngine;

public static class Extend
{
    public static double MapRangeClamped(double value, double inRangeA, double inRangeB, double outRangeA = 0.0, double outRangeB = 1.0)
    {
        double result = (outRangeB - outRangeA) / (inRangeB - inRangeA) * (value - inRangeB) + outRangeB;
        if (value > inRangeB) result = outRangeB;
        else if (value < inRangeA) result = outRangeA;
        return result;
    }
    public static string GetComponentNames(GameObject owningObject)
    {
        Component[] components = owningObject.GetComponents<Component>();
        int count = components.Length;
        string output = "";

        output += $"Component count: {count} |\n";

        for (int i = 0; i < components.Length; i++)
        {
            Component comp = components[i];
            output += $"Type: {comp.GetType()}, component name: {comp.name}, index: {i}, toString: {comp} |\n";
        }

        return output;
    }
}

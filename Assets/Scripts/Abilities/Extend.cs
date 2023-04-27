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

        output += $"Owning object name: {owningObject.name}, type: {owningObject.GetType()}, component count: {count} |\n";

        for (int i = 0; i < count; i++)
        {
            Component comp = components[i];
            output += $"i: {i}, type: {comp.GetType()}, name: {comp.name} |\n";
        }

        return output;
    }
    public static FallCountCanvas FindFallCountCanvas(GameObject gameObject)
    {
        bool found = false;
        GameObject fallCountCanvasObject = null;

        // gameObject should be Devin.
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            // This game object should be FallCountCanvas.
            fallCountCanvasObject = gameObject.transform.GetChild(i).gameObject;

            if (fallCountCanvasObject.name == "FallCountCanvas")
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            if (fallCountCanvasObject.TryGetComponent<FallCountCanvas>(out var fallCountCanvas))
            {
                return fallCountCanvas;
            }
            else
            {
                Debug.Log("Couldn't find FallCountCanvas.");
                return null;
            }
        }
        else
        {
            Debug.Log("Couldn't find FallCountCanvas game object.");
            return null;
        }
    }
}

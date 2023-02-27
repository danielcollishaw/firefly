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

public static class Extend
{
    public static double MapRangeClamped(double value, double inRangeA, double inRangeB, double outRangeA = 0.0, double outRangeB = 1.0)
    {
        double result = (outRangeB - outRangeA) / (inRangeB - inRangeA) * (value - inRangeB) + outRangeB;
        if (value > inRangeB) result = outRangeB;
        else if (value < inRangeA) result = outRangeA;
        return result;
    }
}

﻿using Generators.Input;

namespace Generators.Exercises
{
    public class Rectangles : GeneratorExercise
    {
        protected override void UpdateCanonicalData(CanonicalData canonicalData)
        {
            foreach (var canonicalDataCase in canonicalData.Cases)
            {
                canonicalDataCase.Property = "count";
                canonicalDataCase.Input["strings"] = canonicalDataCase.Input["strings"] as string[] ?? new string[0];
                canonicalDataCase.UseVariablesForInput = true;
            }
        }
    }
}
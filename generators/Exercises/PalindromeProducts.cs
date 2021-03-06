﻿using System;
using System.Collections.Generic;
using System.Linq;
using Generators.Input;
using Generators.Output;
using Humanizer;
using Newtonsoft.Json.Linq;

namespace Generators.Exercises
{
    public class PalindromeProducts : GeneratorExercise
    {
        protected override void UpdateCanonicalData(CanonicalData canonicalData)
        {
            foreach (var canonicalDataCase in canonicalData.Cases)
            {
                if (canonicalDataCase.Expected.ContainsKey("error"))
                {
                    canonicalDataCase.ExceptionThrown = typeof(ArgumentException);
                }
                else
                {
                    canonicalDataCase.UseVariableForTested = true;
                    canonicalDataCase.UseVariableForExpected = true;
                    canonicalDataCase.Expected = (canonicalDataCase.Expected["value"], FormatCoordinates(canonicalDataCase.Expected["factors"]));
                }
            }
        }

        protected override string RenderTestMethodBodyAssert(TestMethodBody testMethodBody)
        {
            if (testMethodBody.CanonicalDataCase.ExceptionThrown != null)
            {
                return base.RenderTestMethodBodyAssert(testMethodBody);
            }

            return string.Join("\n", new[]
            {
                "Assert.Equal(expected.Item1, actual.Item1);",
                "Assert.Equal(expected.Item2, actual.Item2);"
            });
        }

        private string FormatCoordinates(dynamic coordinates)
            => ValueFormatter.Format((coordinates as JArray).Select(coordinate => (coordinate[0].ToObject<int>(), coordinate[1].ToObject<int>())).ToArray());
    }
}
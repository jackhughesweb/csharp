﻿using Generators.Input;
using Generators.Output;

namespace Generators.Exercises
{
    public class Connect : GeneratorExercise
    {
        protected override void UpdateCanonicalData(CanonicalData canonicalData)
        {
            foreach (var canonicalDataCase in canonicalData.Cases)
            {
                canonicalDataCase.UseVariablesForConstructorParameters = true;
                canonicalDataCase.SetConstructorInputParameters("board");
                canonicalDataCase.Property = "result";
                canonicalDataCase.Input["board"] = ToMultiLineString(canonicalDataCase.Input["board"]);

                //convert to enum
                switch (canonicalDataCase.Expected)
                {
                    case "X":
                        canonicalDataCase.Expected = new UnescapedValue("ConnectWinner.Black");
                        break;
                    case "O":
                        canonicalDataCase.Expected = new UnescapedValue("ConnectWinner.White");
                        break;
                    case "":
                        canonicalDataCase.Expected = new UnescapedValue("ConnectWinner.None");
                        break;
                }
            }
        }

        private UnescapedValue ToMultiLineString(string[] input)
        {
            const string template =
@"new [] 
{ 
    {% if input.size == 0 %}string.Empty{% else %}{% for item in {{input}} %}{% if forloop.length == 1 %}""{{item}}""{% break %}{% endif %}""{{item}}""{% if forloop.last == false %},{% else %}{{string.Empty}}{% endif %}
    {% endfor %}{% endif %} 
}";

            return new UnescapedValue(TemplateRenderer.RenderInline(template, new { input }));
        }
    }
}

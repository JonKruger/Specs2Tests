using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Specs2Tests
{
    public class SpecParser : ISpecParser
    {
        public ParsedSpec Parse(string specText)
        {
            var result = new ParsedSpec();
            var currentSpecGroup = new ParsedSpec.SpecGroup();
            foreach (var line in specText.Split(new[] {"\r\n"}, StringSplitOptions.None).Select(s => s.Trim()))
            {
                if (string.IsNullOrEmpty(line))
                {
                    currentSpecGroup = null;
                    continue;
                }

                if (line.StartsWith("-"))
                {
                    var match = Regex.Match(line, @"^-\s*?([^\s].*)$");
                    if (match.Success)
                        currentSpecGroup.AddTest(match.Groups[1].Value);
                }
                else if (currentSpecGroup == null)
                {
                    currentSpecGroup = result.AddSpecGroup();
                    currentSpecGroup.ClassName = line;
                }
            }
            return result;
        }
    }
}
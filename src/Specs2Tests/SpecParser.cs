using System;
using System.Text.RegularExpressions;

namespace Specs2Tests
{
    public class SpecParser
    {
        public ParsedSpec Parse(string specText)
        {
            var result = new ParsedSpec();

            foreach (var line in specText.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.StartsWith("-"))
                {
                    var match = Regex.Match(line, @"^-\s*?([^\s].*)$");
                    if (match.Success)
                        result.AddTest(match.Groups[1].Value);
                }
                else if (string.IsNullOrEmpty(result.ClassName))
                {
                    result.ClassName = line;
                }
            }
            return result;
        }
    }
}
using System.Text.RegularExpressions;

namespace Specs2Tests
{
    public class SpecParser : ISpecParser
    {
        private bool _inScenario = false;
        private Scenario _currentScenario;
        private string _currentPhraseStarter;

        public ParsedSpec Parse(string specText)
        {
            var parsedSpec = new ParsedSpec();
            foreach (var line in specText.SplitIntoLines())
            {
                if (string.IsNullOrEmpty(line.Trim()))
                    continue;

                if (IsScenarioLine(line))
                {
                    if (_inScenario)
                        parsedSpec.AddScenario(_currentScenario);

                    _currentScenario = new Scenario();

                    var match = Regex.Match(line.Trim(), "^Scenario:(.*)$");
                    _currentScenario.Name = StringifyMethodName(match.Groups[1].Value.Trim());
                    _inScenario = true;
                }
                else
                {
                    var method = StringifyMethodName(line.Trim());
                    method = ReplaceWithPhraseStarter(method);
                    _currentScenario.CalledMethods.Add(method);
                    parsedSpec.AddHelperMethod(method);
                }
            }

            if (!string.IsNullOrEmpty(_currentScenario.Name))
                parsedSpec.AddScenario(_currentScenario);

            return parsedSpec;
        }

        private string ReplaceWithPhraseStarter(string line)
        {
            var match = Regex.Match(line, "^([^_]+)(_.*)$");
            var foundPhraseStarter = match.Groups[1].Value;

            switch (foundPhraseStarter.ToLower())
            {
                case "given": case "when": case "then":
                    _currentPhraseStarter = foundPhraseStarter;
                    return line;
                default:
                    return string.Format("{0}{1}", _currentPhraseStarter, match.Groups[2].Value);
            }
        }

        private bool IsScenarioLine(string line)
        {
            return Regex.IsMatch(line.Trim(), "^Scenario:(.*)$");
        }

        public static string StringifyMethodName(string text)
        {
            Match match;
            var cleanMethodName = Regex.Replace(text, "[^A-Za-z0-9_]", "_");
            while (Regex.IsMatch(cleanMethodName, "__"))
                cleanMethodName = cleanMethodName.Replace("__", "_");
            if ((match = Regex.Match(cleanMethodName, "^(.*)_+$")).Success)
                cleanMethodName = match.Groups[1].Value;
            return cleanMethodName;
        }
    }
}
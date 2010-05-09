using System.Text;

namespace Specs2Tests
{
    public class CodeWriter : ICodeWriter
    {
        public string WriteCode(ParsedSpec parsedSpec)
        {
            var result = new StringBuilder();

            var firstSpecGroup = true;
            foreach (var specGroup in parsedSpec.SpecGroups)
            {
                if (!firstSpecGroup)
                    result.AppendLine();
                firstSpecGroup = false;

                result.AppendLine("[TestFixture]");
                result.AppendLine(string.Format("public class {0}", AddUnderscores(specGroup.ClassName)));
                result.AppendLine("{");

                var firstTest = true;
                foreach (var testName in specGroup.TestNames)
                {
                    if (!firstTest)
                        result.AppendLine();
                    firstTest = false;

                    result.AppendLine("    [Test]");
                    result.AppendLine(string.Format("    public void {0}()", AddUnderscores(testName)));
                    result.AppendLine("    {");
                    result.AppendLine();
                    result.AppendLine("    }");
                }

                result.AppendLine("}");
            }
            return result.ToString();
        }

        private string AddUnderscores(string s)
        {
            return s.Replace(" ", "_");
        }
    }
}
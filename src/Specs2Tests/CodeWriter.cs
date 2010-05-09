using System.Text;

namespace Specs2Tests
{
    public class CodeWriter : ICodeWriter
    {
        private readonly IConfiguration _configuration;

        public CodeWriter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
                WriteTestNameLine(result, specGroup);

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

        private void WriteTestNameLine(StringBuilder result, ParsedSpec.SpecGroup specGroup)
        {
            result.AppendFormat("public class {0}", AddUnderscores(specGroup.ClassName));

            if (!string.IsNullOrEmpty(_configuration.BaseTestClass))
                result.AppendFormat(" : {0}", _configuration.BaseTestClass);
            result.AppendLine();
        }

        private string AddUnderscores(string s)
        {
            return s.Replace(" ", "_");
        }
    }
}
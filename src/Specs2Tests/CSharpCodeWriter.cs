using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Specs2Tests
{
    public class CSharpCodeWriter : ICSharpCodeWriter
    {
        private readonly IConfiguration _configuration;

        public CSharpCodeWriter(IConfiguration configuration)
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

                if (specGroup.SetupMethods.Any())
                {
                    result.AppendLine("    protected override void Establish_context()");
                    result.AppendLine("    {");
                    foreach (var setupMethod in specGroup.SetupMethods)
                        result.AppendLine("        " + AddUnderscores(setupMethod) + "();");
                    result.AppendLine("    }");
                    result.AppendLine();
                }

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
            return Regex.Replace(s, "[^A-Za-z0-9_]", "_");
        }
    }
}
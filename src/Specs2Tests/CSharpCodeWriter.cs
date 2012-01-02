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
                var firstTest = true;
            foreach (var scenario in parsedSpec.Scenarios)
            {
                if (!firstTest)
                    result.AppendLine();
                firstTest = false;

                result.AppendLine("    [Test]");
                result.AppendLine(string.Format("    public void {0}()", scenario.Name));
                result.AppendLine("    {");

                foreach (var method in scenario.CalledMethods)
                    result.AppendLine(string.Format("        {0}();", method));
 
                result.AppendLine("    }");
            }

            if (parsedSpec.HelperMethods.Any())
            {
                result.AppendLine();
                result.AppendLine("    // Helper methods");
            }

            foreach (var method in parsedSpec.HelperMethods)
            {
                result.AppendLine();
                result.AppendLine(string.Format("    private void {0}()", method));
                result.AppendLine("    {");
                result.AppendLine("        throw new NotImplementedException();");
                result.AppendLine("    }");
            }
            return result.ToString();
        }
    }
}
using System.Collections.Generic;

namespace Specs2Tests
{
    public class ParsedSpec
    {
        private List<string> _testNames = new List<string>();

        public string ClassName{get; set;}
        public IEnumerable<string> TestNames
        {
            get { return _testNames; }
        }

        public void AddTest(string testName)
        {
            _testNames.Add(testName);
        }
    }
}
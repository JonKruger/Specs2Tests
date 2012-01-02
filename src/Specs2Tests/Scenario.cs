using System.Collections.Generic;

namespace Specs2Tests
{
    public class Scenario
    {
        public string Name { get; set; }
        public IList<string> CalledMethods { get; set; }

        public Scenario()
        {
            CalledMethods = new List<string>();
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Specs2Tests
{
    public class ParsedSpec 
    {
        public IList<Scenario> Scenarios
        {
            get { return _scenarios.AsReadOnly(); }
        }

        public IList<string> HelperMethods
        {
            get
            {
                var result = new List<string>();
                result.AddRange(_helperMethods.Where(m => m.ToLower().StartsWith("given")));
                result.AddRange(_helperMethods.Where(m => m.ToLower().StartsWith("when")));
                result.AddRange(_helperMethods.Where(m => m.ToLower().StartsWith("then")));
                return result.Distinct().ToList().AsReadOnly();
            }
        }

        private List<Scenario> _scenarios = new List<Scenario>(); 
        private List<string> _helperMethods = new List<string>();

        public void AddScenario(Scenario scenario)
        {
            _scenarios.Add(scenario);
        }

        public void AddHelperMethod(string method)
        {
            _helperMethods.Add(method);
        }
    }
}
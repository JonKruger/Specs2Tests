using System;
using System.Collections.Generic;

namespace Specs2Tests
{
    public class ParsedSpec
    {
        public class SpecGroup
        {
            private List<string> _testNames = new List<string>();

            public string ClassName { get; set; }
            public IEnumerable<string> TestNames
            {
                get { return _testNames; }
            }

            public void AddTest(string testName)
            {
                _testNames.Add(testName);
            }
        }

        private List<SpecGroup> _specGroups = new List<SpecGroup>();

        public IEnumerable<SpecGroup> SpecGroups
        {
            get { return _specGroups; }
        }

        public SpecGroup AddSpecGroup()
        {
            var specGroup = new SpecGroup();
            _specGroups.Add(specGroup);
            return specGroup;
        }
    }
}
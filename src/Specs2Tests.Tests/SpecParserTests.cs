using System.Linq;
using System.Text;
using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace Specs2Tests.Tests
{
    [TestFixture]
    public class When_parsing_specs : Specification
    {
        private SpecParser _parser;
        private ParsedSpec _result;

        protected override void Establish_context()
        {
            _parser = new SpecParser();
        }

        protected override void Because_of()
        {
            _result = _parser.Parse(
                @"
When attempting to close an account and the account balance is not zero
- should not mark the account as closed
- should notify the user that the account could not be closed because the account balance was not zero");
        }

        [Test]
        public void The_first_line_is_the_class_name()
        {
            _result.ClassName.ShouldEqual("When attempting to close an account and the account balance is not zero");
        }

        [Test]
        public void Subsequent_lines_starting_with_dashes_are_the_test_names()
        {
            _result.TestNames.Count().ShouldEqual(2);
            _result.TestNames.ElementAt(0).ShouldEqual("should not mark the account as closed");
            _result.TestNames.ElementAt(1).ShouldEqual("should notify the user that the account could not be closed because the account balance was not zero");
        }
    }
}

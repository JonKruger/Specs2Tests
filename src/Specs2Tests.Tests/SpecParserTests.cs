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
- should notify the user that the account could not be closed because the account balance was not zero

    When attempting to close an account and the account balance is zero
    - should mark the account as closed
    - should notify the user that the account was successfully closed 
");
        }

        [Test]
        public void The_first_line_in_a_section_is_the_class_name()
        {
            _result.SpecGroups.ElementAt(0).ClassName.ShouldEqual("When attempting to close an account and the account balance is not zero");
        }

        [Test]
        public void Subsequent_lines_starting_with_dashes_are_the_test_names()
        {
            _result.SpecGroups.ElementAt(0).TestNames.Count().ShouldEqual(2);
            _result.SpecGroups.ElementAt(0).TestNames.ElementAt(0).ShouldEqual("should not mark the account as closed");
            _result.SpecGroups.ElementAt(0).TestNames.ElementAt(1).ShouldEqual("should notify the user that the account could not be closed because the account balance was not zero");
        }

        [Test]
        public void When_a_line_follows_a_line_break_it_is_a_new_test()
        {
            _result.SpecGroups.ElementAt(1).ClassName.ShouldEqual("When attempting to close an account and the account balance is zero");
        }

        [Test]
        public void Should_parse_the_test_names_for_the_second_set_of_specs()
        {
            _result.SpecGroups.ElementAt(1).TestNames.Count().ShouldEqual(2);
            _result.SpecGroups.ElementAt(1).TestNames.ElementAt(0).ShouldEqual("should mark the account as closed");
            _result.SpecGroups.ElementAt(1).TestNames.ElementAt(1).ShouldEqual("should notify the user that the account was successfully closed");
        }
    }

    [TestFixture]
    public class Another_spec_parsing_test : Specification
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
                @"When depositing money into an account
- should add the specified amount into the account
 
When withdrawing money from an account 
- should withdraw the specified amount from the account
- should specify that the withdrawal was successful");
        }

        [Test]
        public void The_first_line_in_a_section_is_the_class_name()
        {
            _result.SpecGroups.ElementAt(0).ClassName.ShouldEqual("When depositing money into an account");
        }

        [Test]
        public void Subsequent_lines_starting_with_dashes_are_the_test_names()
        {
            _result.SpecGroups.ElementAt(0).TestNames.Count().ShouldEqual(1);
            _result.SpecGroups.ElementAt(0).TestNames.ElementAt(0).ShouldEqual("should add the specified amount into the account");
        }

        [Test]
        public void When_a_line_follows_a_line_break_it_is_a_new_test()
        {
            _result.SpecGroups.ElementAt(1).ClassName.ShouldEqual("When withdrawing money from an account");
        }

        [Test]
        public void Should_parse_the_test_names_for_the_second_set_of_specs()
        {
            _result.SpecGroups.ElementAt(1).TestNames.Count().ShouldEqual(2);
            _result.SpecGroups.ElementAt(1).TestNames.ElementAt(0).ShouldEqual("should withdraw the specified amount from the account");
            _result.SpecGroups.ElementAt(1).TestNames.ElementAt(1).ShouldEqual("should specify that the withdrawal was successful");
        }

    }

    [TestFixture]
    public class When_parsing_specs_with_Given_and_When : Specification
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
Given something cool
When doing something awesome
- should revel in the awesomeness
");
        }

        [Test]
        public void Should_use_the_Given_as_the_base_class_for_the_When()
        {
            _result.SpecGroups.ElementAt(0).ClassName.ShouldEqual("Given something cool");
            _result.SpecGroups.ElementAt(0).TestNames.ShouldBeEmpty();

            _result.SpecGroups.ElementAt(1).ClassName.ShouldEqual("When doing something awesome");
            _result.SpecGroups.ElementAt(1).BaseClass.ShouldEqual("Given something cool");
            _result.SpecGroups.ElementAt(1).TestNames.Single().ShouldEqual("should revel in the awesomeness");
        }
    }
}

using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace Specs2Tests.Tests
{
    [TestFixture]
    public class When_writing_out_the_code_in_C_sharp : Specification
    {
        private CSharpCodeWriter _writer;
        private ParsedSpec _parsedSpec;
        private string _result;

        protected override void Establish_context()
        {
            _parsedSpec = new ParsedSpec();
            var currentSpecGroup = _parsedSpec.AddSpecGroup();
            currentSpecGroup.ClassName = "When attempting to close an account and the account balance is not zero";
            currentSpecGroup.AddTest("should not mark the account as closed");
            currentSpecGroup.AddTest("should notify the user that the account could not be closed because the account balance was not zero");

            currentSpecGroup = _parsedSpec.AddSpecGroup();
            currentSpecGroup.ClassName = "When doing something cool";
            currentSpecGroup.AddTest("should do something awesome");

            var configuration = CreateStub<IConfiguration>();
            configuration.Stub(s => s.BaseTestClass).Return("BaseTestClass");

            _writer = new CSharpCodeWriter(configuration);
        }

        protected override void Because_of()
        {
            _result = _writer.WriteCode(_parsedSpec);
        }

        [Test]
        public void Should_create_the_test_code()
        {
            _result.ShouldEqual(
                @"[TestFixture]
public class When_attempting_to_close_an_account_and_the_account_balance_is_not_zero : BaseTestClass
{
    [Test]
    public void should_not_mark_the_account_as_closed()
    {

    }

    [Test]
    public void should_notify_the_user_that_the_account_could_not_be_closed_because_the_account_balance_was_not_zero()
    {

    }
}

[TestFixture]
public class When_doing_something_cool : BaseTestClass
{
    [Test]
    public void should_do_something_awesome()
    {

    }
}
");
        }
    }

    [TestFixture]
    public class When_writing_out_the_code_in_C_sharp_and_the_parsed_text_has_characters_that_cannot_be_in_class_or_method_names : Specification
    {
        private CSharpCodeWriter _writer;
        private ParsedSpec _parsedSpec;
        private string _result;

        protected override void Establish_context()
        {
            _parsedSpec = new ParsedSpec();
            var currentSpecGroup = _parsedSpec.AddSpecGroup();
            currentSpecGroup.ClassName = "When, \"a'b+c^d%e#f-";

            var configuration = CreateStub<IConfiguration>();
            configuration.Stub(s => s.BaseTestClass).Return("BaseTestClass");

            _writer = new CSharpCodeWriter(configuration);
        }

        protected override void Because_of()
        {
            _result = _writer.WriteCode(_parsedSpec);
        }

        [Test]
        public void Should_replace_the_special_characters_with_underscores()
        {
            _result.ShouldEqual(
                @"[TestFixture]
public class When___a_b_c_d_e_f_ : BaseTestClass
{
}
");
        }
    }
}


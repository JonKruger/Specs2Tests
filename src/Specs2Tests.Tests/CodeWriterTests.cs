using NBehave.Spec.NUnit;
using NUnit.Framework;

namespace Specs2Tests.Tests
{
    [TestFixture]
    public class When_writing_out_the_code : Specification
    {
        private CodeWriter _writer;
        private ParsedSpec _parsedSpec;
        private string _result;

        protected override void Establish_context()
        {
            _parsedSpec = new ParsedSpec();
            _parsedSpec.ClassName = "When attempting to close an account and the account balance is not zero";
            _parsedSpec.AddTest("should not mark the account as closed");
            _parsedSpec.AddTest("should notify the user that the account could not be closed because the account balance was not zero");

            _writer = new CodeWriter();
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
public class When_attempting_to_close_an_account_and_the_account_balance_is_not_zero
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
");
        }
    }
}
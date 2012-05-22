using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace Specs2Tests.Tests
{
    [TestFixture]
    public class When_writing_out_the_code_in_vb_for_MSTest : Specification
    {
        private VBCodeWriter _writer;
        private ParsedSpec _parsedSpec;
        private string _result;

        protected override void Establish_context()
        {
            _parsedSpec = new ParsedSpec();
            _parsedSpec.AddScenario(new Scenario
                                    {
                                        Name = "Test_name",
                                        CalledMethods = new[]
                                                            {
                                                                "Given_something",
                                                                "When_I_do_something",
                                                                "Then_something_should_happen"
                                                            }
                                    });
            _parsedSpec.AddHelperMethod("Given_something");
            _parsedSpec.AddHelperMethod("When_I_do_something");
            _parsedSpec.AddHelperMethod("Then_something_should_happen");

            _parsedSpec.AddScenario(new Scenario
                                    {
                                        Name = "Another_test_name",
                                        CalledMethods = new[]
                                                            {
                                                                "Given_something_else",
                                                                "When_I_do_something_else",
                                                                "Then_something_else_should_happen"
                                                            }
                                    });
            _parsedSpec.AddHelperMethod("Given_something");
            _parsedSpec.AddHelperMethod("When_I_do_something");
            _parsedSpec.AddHelperMethod("Then_something_should_happen");
            _parsedSpec.AddHelperMethod("Given_something_else");
            _parsedSpec.AddHelperMethod("When_I_do_something_else");
            _parsedSpec.AddHelperMethod("Then_something_else_should_happen");

            _writer = new VBCodeWriter(new MSTestConfiguration());
        }

        protected override void Because_of()
        {
            _result = _writer.WriteCode(_parsedSpec);
        }

        [Test]
        public void Should_create_the_test_code()
        {
            _result.ShouldEqual(
                @"    <TestMethod()>
    Public Sub Test_name()
        Given_something()
        When_I_do_something()
        Then_something_should_happen()
    End Sub

    <TestMethod()>
    Public Sub Another_test_name()
        Given_something_else()
        When_I_do_something_else()
        Then_something_else_should_happen()
    End Sub

    #region Helper methods

    Private Sub Given_something()
        throw new NotImplementedException()
    End Sub

    Private Sub Given_something_else()
        throw new NotImplementedException()
    End Sub

    Private Sub When_I_do_something()
        throw new NotImplementedException()
    End Sub

    Private Sub When_I_do_something_else()
        throw new NotImplementedException()
    End Sub

    Private Sub Then_something_should_happen()
        throw new NotImplementedException()
    End Sub

    Private Sub Then_something_else_should_happen()
        throw new NotImplementedException()
    End Sub

    #endregion
");
        }
    }
    [TestFixture]
    public class When_writing_out_the_code_in_vb_for_NUnit : Specification
    {
        private VBCodeWriter _writer;
        private ParsedSpec _parsedSpec;
        private string _result;

        protected override void Establish_context()
        {
            _parsedSpec = new ParsedSpec();
            _parsedSpec.AddScenario(new Scenario
                                    {
                                        Name = "Test_name",
                                        CalledMethods = new[]
                                                            {
                                                                "Given_something",
                                                                "When_I_do_something",
                                                                "Then_something_should_happen"
                                                            }
                                    });
            _parsedSpec.AddHelperMethod("Given_something");
            _parsedSpec.AddHelperMethod("When_I_do_something");
            _parsedSpec.AddHelperMethod("Then_something_should_happen");

            _parsedSpec.AddScenario(new Scenario
                                    {
                                        Name = "Another_test_name",
                                        CalledMethods = new[]
                                                            {
                                                                "Given_something_else",
                                                                "When_I_do_something_else",
                                                                "Then_something_else_should_happen"
                                                            }
                                    });
            _parsedSpec.AddHelperMethod("Given_something");
            _parsedSpec.AddHelperMethod("When_I_do_something");
            _parsedSpec.AddHelperMethod("Then_something_should_happen");
            _parsedSpec.AddHelperMethod("Given_something_else");
            _parsedSpec.AddHelperMethod("When_I_do_something_else");
            _parsedSpec.AddHelperMethod("Then_something_else_should_happen");

            _writer = new VBCodeWriter(new NUnitConfiguration());
        }

        protected override void Because_of()
        {
            _result = _writer.WriteCode(_parsedSpec);
        }

        [Test]
        public void Should_create_the_test_code()
        {
            _result.ShouldEqual(
                @"    <Test()>
    Public Sub Test_name()
        Given_something()
        When_I_do_something()
        Then_something_should_happen()
    End Sub

    <Test()>
    Public Sub Another_test_name()
        Given_something_else()
        When_I_do_something_else()
        Then_something_else_should_happen()
    End Sub

    #region Helper methods

    Private Sub Given_something()
        throw new NotImplementedException()
    End Sub

    Private Sub Given_something_else()
        throw new NotImplementedException()
    End Sub

    Private Sub When_I_do_something()
        throw new NotImplementedException()
    End Sub

    Private Sub When_I_do_something_else()
        throw new NotImplementedException()
    End Sub

    Private Sub Then_something_should_happen()
        throw new NotImplementedException()
    End Sub

    Private Sub Then_something_else_should_happen()
        throw new NotImplementedException()
    End Sub

    #endregion
");
        }
    }
}


using NBehave.Spec.NUnit;
using NUnit.Framework;
using Rhino.Mocks;

namespace Specs2Tests.Tests
{
    [TestFixture]
    public class When_writing_out_the_code_in_C_sharp_for_MSTest : Specification
    {
        private CSharpCodeWriter _writer;
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

            _writer = new CSharpCodeWriter(new MSTestConfiguration());
        }

        protected override void Because_of()
        {
            _result = _writer.WriteCode(_parsedSpec);
        }

        [Test]
        public void Should_create_the_test_code()
        {
            _result.ShouldEqual(
                @"    [TestMethod]
    public void Test_name()
    {
        Given_something();
        When_I_do_something();
        Then_something_should_happen();
    }

    [TestMethod]
    public void Another_test_name()
    {
        Given_something_else();
        When_I_do_something_else();
        Then_something_else_should_happen();
    }

    // Helper methods

    private void Given_something()
    {
        throw new NotImplementedException();
    }

    private void Given_something_else()
    {
        throw new NotImplementedException();
    }

    private void When_I_do_something()
    {
        throw new NotImplementedException();
    }

    private void When_I_do_something_else()
    {
        throw new NotImplementedException();
    }

    private void Then_something_should_happen()
    {
        throw new NotImplementedException();
    }

    private void Then_something_else_should_happen()
    {
        throw new NotImplementedException();
    }
");
        }
    }
    [TestFixture]
    public class When_writing_out_the_code_in_C_sharp_for_NUnit : Specification
    {
        private CSharpCodeWriter _writer;
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

            _writer = new CSharpCodeWriter(new NUnitConfiguration());
        }

        protected override void Because_of()
        {
            _result = _writer.WriteCode(_parsedSpec);
        }

        [Test]
        public void Should_create_the_test_code()
        {
            _result.ShouldEqual(
                @"    [Test]
    public void Test_name()
    {
        Given_something();
        When_I_do_something();
        Then_something_should_happen();
    }

    [Test]
    public void Another_test_name()
    {
        Given_something_else();
        When_I_do_something_else();
        Then_something_else_should_happen();
    }

    // Helper methods

    private void Given_something()
    {
        throw new NotImplementedException();
    }

    private void Given_something_else()
    {
        throw new NotImplementedException();
    }

    private void When_I_do_something()
    {
        throw new NotImplementedException();
    }

    private void When_I_do_something_else()
    {
        throw new NotImplementedException();
    }

    private void Then_something_should_happen()
    {
        throw new NotImplementedException();
    }

    private void Then_something_else_should_happen()
    {
        throw new NotImplementedException();
    }
");
        }
    }
}


using System;
using NUnit.Framework;
using Rhino.Mocks;

namespace Specs2Tests.Tests
{
    public class When_the_application_is_run : Specification
    {
        private IClipboard _clipboard;
        private ISpecParser _specParser;
        private ICodeWriter _cSharpCodeWriter;
        private Application _application;

        protected override void Establish_context()
        {
            _clipboard = CreateStub<IClipboard>();
            _specParser = CreateStub<ISpecParser>();
            _cSharpCodeWriter = CreateStub<ICodeWriter>();
            _application = new Application(_clipboard, _specParser, _cSharpCodeWriter);

            var parsedSpec = new ParsedSpec();

            _clipboard.Stub(c => c.GetData()).Return("spec");
            _specParser.Stub(p => p.Parse("spec")).Return(parsedSpec);
            _cSharpCodeWriter.Stub(w => w.WriteCode(parsedSpec)).Return("code");
        }

        protected override void Because_of()
        {
            _application.Run();
        }

        [Test]
        public void Should_take_the_textual_specs_off_of_the_clipboard_and_put_the_test_code_onto_the_clipboard()
        {
            _clipboard.AssertWasCalled(c => c.SetData("code"));
        }
    }
}
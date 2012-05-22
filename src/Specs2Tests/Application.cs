namespace Specs2Tests
{
    public class Application : IApplication
    {
        private readonly IClipboard _clipboard;
        private readonly ISpecParser _specParser;
        private readonly ICodeWriter _codeWriter;

        public Application(IClipboard clipboard, ISpecParser specParser, ICodeWriter cSharpCodeWriter)
        {
            _clipboard = clipboard;
            _specParser = specParser;
            _codeWriter = cSharpCodeWriter;
        }

        public void Run()
        {
            var specs = GetDataFromClipboard();
            var parsedSpec = ParseSpecs(specs);
            var code = GetTheCode(parsedSpec);
            SaveCodeToClipboard(code);
        }

        private string GetDataFromClipboard()
        {
            return _clipboard.GetData();
        }

        private ParsedSpec ParseSpecs(string specs)
        {
            return _specParser.Parse(specs);
        }

        private string GetTheCode(ParsedSpec parsedSpec)
        {
            return _codeWriter.WriteCode(parsedSpec);
        }

        private void SaveCodeToClipboard(string code)
        {
            _clipboard.SetData(code);
        }
    }
}
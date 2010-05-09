namespace Specs2Tests
{
    public class Application : IApplication
    {
        private readonly IClipboard _clipboard;
        private readonly ISpecParser _specParser;
        private readonly ICSharpCodeWriter _cSharpCodeWriter;

        public Application(IClipboard clipboard, ISpecParser specParser, ICSharpCodeWriter cSharpCodeWriter)
        {
            _clipboard = clipboard;
            _specParser = specParser;
            _cSharpCodeWriter = cSharpCodeWriter;
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
            return _cSharpCodeWriter.WriteCode(parsedSpec);
        }

        private void SaveCodeToClipboard(string code)
        {
            _clipboard.SetData(code);
        }
    }
}
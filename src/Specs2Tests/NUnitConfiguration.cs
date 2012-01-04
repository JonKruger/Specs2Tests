namespace Specs2Tests
{
    public class NUnitConfiguration : IConfiguration
    {
        public string ClassAttributeClass
        {
            get { return "TestFixture"; }
        }

        public string TestAttributeClass
        {
            get { return "Test"; }
        }
    }
}
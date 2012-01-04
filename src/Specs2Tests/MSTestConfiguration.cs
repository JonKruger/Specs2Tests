namespace Specs2Tests
{
    public class MSTestConfiguration : IConfiguration
    {
        public string ClassAttributeClass
        {
            get { return "TestClass"; }
        }

        public string TestAttributeClass
        {
            get { return "TestMethod"; }
        }
    }
}
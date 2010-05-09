using System;
using System.Configuration;

namespace Specs2Tests
{
    public interface IConfiguration
    {
        string BaseTestClass { get; }
    }

    public class Configuration : IConfiguration
    {
        public string BaseTestClass
        {
            get { return ConfigurationManager.AppSettings["BaseTestClass"]; }
        }
    }
}
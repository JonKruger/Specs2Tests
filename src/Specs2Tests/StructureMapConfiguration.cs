using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using StructureMap;

namespace Specs2Tests
{
    public class StructureMapConfiguration
    {
        public void Configure()
        {
            ObjectFactory.Initialize(
                x =>
                    {
                        x.Scan(scan =>
                                   {
                                       scan.WithDefaultConventions();
                                       scan.Assembly(GetType().Assembly);
                                   });

                        ConfigureConfiguration(x);
                    });
        }

        private void ConfigureConfiguration(IInitializationExpression x)
        {
            var outputLanguage = ConfigurationManager.AppSettings["OutputLanguage"];
            switch (outputLanguage)
            {
                case "C#":
                    x.For<ICodeWriter>().Use<CSharpCodeWriter>();
                    break;
                case "VB":
                    x.For<ICodeWriter>().Use<VBCodeWriter>();
                    break;
                default:
                    throw new NotImplementedException("OutputLanguage value in config file must be one of the following: C#, VB.");
            }

            var testFramework = ConfigurationManager.AppSettings["TestFramework"];
            switch (testFramework)
            {
                case "MSTest":
                    x.For<IConfiguration>().Use<MSTestConfiguration>();
                    break;
                case "NUnit":
                    x.For<IConfiguration>().Use<NUnitConfiguration>();
                    break;
                default:
                    throw new NotImplementedException("TestFramework value in config file must be one of the following: MSTest, NUnit.");
            }
        }

        public void Reset()
        {
            ObjectFactory.Initialize(x => { });
        }
    }
}
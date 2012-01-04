using System;
using System.Configuration;

namespace Specs2Tests
{
    public interface IConfiguration
    {
        string ClassAttributeClass { get; }
        string TestAttributeClass { get; }
    }
}
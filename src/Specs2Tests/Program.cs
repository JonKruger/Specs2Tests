using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;

namespace Specs2Tests
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            new StructureMapConfiguration().Configure();

            var application = ObjectFactory.GetInstance<IApplication>();
            application.Run();
        }
    }
}

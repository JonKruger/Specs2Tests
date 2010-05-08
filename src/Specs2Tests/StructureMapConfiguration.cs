using System;
using System.Collections.Generic;
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
                    });
        }

        public void Reset()
        {
            ObjectFactory.Initialize(x => { });
        }
    }
}
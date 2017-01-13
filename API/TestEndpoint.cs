using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API
{
    public class TestEndpoint : NancyModule
    {
        public TestEndpoint()
        {
            Get["/"] = parameters =>
            {
                return "Hello World!";
            };
        }
    }
}
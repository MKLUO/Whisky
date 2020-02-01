using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Server.Controllers {
    public class TestController : ApiController {

        public struct Value {
            public string Name { get; set; }
        }

        public HttpResponseMessage Post([FromBody] Value value) {
            Test.TestPrint(value.Name);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}

using ARApp.WebApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace ARApp.WebApi.Controllers
{

    public class CustomerController : ApiController
    {
        // GET api/values
        /*
            User-Agent: Fiddler
            Host: localhost:49814
            Authorization: basic QW1pdDpSZWRrYXI=

            Note : QW1pdDpSZWRrYXI= , this strind is encoded using base64encoding using Encoding.GetEncoding("iso-8859-1");
            check=> https://www.base64encode.org/
             */
        [CustomAuthenticateAttribute]
        public IEnumerable<Customer> Get()
        {
            List<Customer> custList = new List<Customer>();
            custList.Add(new Customer() { id = 1, name = "AR", address = "khar" });
            custList.Add(new Customer() { id = 2, name = "TJ", address = "KGBK" });
            custList.Add(new Customer() { id = 3, name = "YI", address = "khioy" });
            custList.Add(new Customer() { id = 4, name = "SR", address = "dtrt" });
            return custList.AsEnumerable();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
    }
}

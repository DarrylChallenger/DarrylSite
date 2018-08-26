using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web.Http;

namespace DarrylSite
{
    [DataContract]
    public class StripeTokenId
    {
        [DataMember]
        public string token { get; set; }
    }
    public class StripeMsgController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }


        // POST api/<controller>
        [HttpPost]
        public async Task Post([FromBody] StripeTokenId tok)
        {
            string json = await Request.Content.ReadAsStringAsync();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
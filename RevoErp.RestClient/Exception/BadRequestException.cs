using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevoErp.RestClient.Exception
{
    public class BadRequestException : System.Exception
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public new string Message { get; set; }
        public BadRequestException() { }
        public static BadRequestException Create(string raw)
        {
            return JsonConvert.DeserializeObject<BadRequestException>(raw);
        }
    }
}

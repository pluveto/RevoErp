using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevoErp.RestClient.Model.Response
{
    public class StaffRet
    {
        public StaffRet() { }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("customId")]
        public string CustomId { get; set; }
        [JsonProperty("screenName")]
        public string ScreenName { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

    }
}

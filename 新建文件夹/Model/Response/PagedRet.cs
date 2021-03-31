using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevoErp.RestClient.Model.Response
{
    class PagedRet
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
        [JsonProperty("data")]
        public dynamic Data { get; set; }
    }
}

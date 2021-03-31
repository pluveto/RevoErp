using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RevoErp.RestClient.Model.Request
{
    class PagedReq
    {
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
        [JsonProperty("countTotal")]
        public bool CountTotal { get; set; }
    }
}

using RestSharp;
using RestSharp.Extensions;
using RevoErp.RestClient.Exception;
using RevoErp.RestClient.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RevoErp.RestClient.Api
{
    public class BasicInfo
    {
        ApiManager api;
        public BasicInfo(ApiManager api)
        {
            this.api = api;
        }

        public async Task<List<StaffRet>> GetStaffs()
        {
            Console.WriteLine("调用");
            var request = new RestRequest("staffs");
            return await api.ExecuteAsync<List<StaffRet>>(request);
        }


    }
}

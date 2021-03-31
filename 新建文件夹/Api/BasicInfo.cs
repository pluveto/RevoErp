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
        IApi api;
        public BasicInfo(IApi api)
        {
            this.api = api;
        }

        public async Task<List<StaffRet>> GetStaffs()
        {
            Console.WriteLine("调用");
            var resp = await api.GetStaffs();
            try
            {
                return resp.GetContent();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw BadRequestException.Create(resp.StringContent);
            }
        }
    }
}

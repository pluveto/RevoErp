using RevoErp.RestClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RevoErp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Init();
        }
        public static ApiManager ApiManager { get; private set; }
        public void Init()
        {
            App.ApiManager = new ApiManager("http://mock-api.com/ZgYZBenN.mock/");
        }
    }
}

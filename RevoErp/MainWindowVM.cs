using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using HandyControl.Data;
using RevoErp._Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevoErp
{
    public class MainWindowVM : ViewModelBase
    {
        public RelayCommand<FunctionEventArgs<object>> SwitchItemCmd => new Lazy<RelayCommand<FunctionEventArgs<object>>>(() =>
            new RelayCommand<FunctionEventArgs<object>>(SwitchItem)).Value;

        private void SwitchItem(FunctionEventArgs<object> info) {
            var window = Application.Current.Windows.OfType<MainWindow>().SingleOrDefault(x => x.IsActive);
            try
            {
                window.Frame.Navigate(new Uri("Pages" + (info.Info as SideMenuItem)?.Tag.ToString(), UriKind.Relative));
            }
            catch (NullReferenceException ex)
            {
                Growl.ErrorGlobal("尚未实现此功能");
            }
            
            //MessageBox.Show((info.Info as SideMenuItem)?.Header.ToString());
        }

        public RelayCommand<string> SelectCmd => new Lazy<RelayCommand<string>>(() =>
            new RelayCommand<string>(Select)).Value;

        private void Select(string header) => Growl.Success(header);
    }
}

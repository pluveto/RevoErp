using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using RevoErp.Pages.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevoErp._Utils
{
    public class ViewModelLocator
    {
         /// <summary>
         /// Initializes a new instance of the ViewModelLocator class.
         /// </summary>
         public ViewModelLocator()
         {
             ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default); 
 
             SimpleIoc.Default.Register<MainWindowVM>();
            SimpleIoc.Default.Register<StaffListPageVM>();
        }
        public MainWindowVM MainWindow => SimpleIoc.Default.GetInstance<MainWindowVM>();
        public StaffListPageVM StaffListPage => SimpleIoc.Default.GetInstance<StaffListPageVM>();

        public static void Cleanup()
         {
             // TODO Clear the ViewModels
         }
    }
}

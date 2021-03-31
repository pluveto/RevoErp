using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using RevoErp.RestClient.Model.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RevoErp.Pages.Settings
{
    public class StaffListPageVM : ViewModelBase, INotifyPropertyChanged
    {
        public Cursor Cursor { get; set; } = Cursors.Arrow;

        #region VM Data
        private ObservableCollection<StaffRet> _staffs = new ObservableCollection<StaffRet>();
        public ObservableCollection<StaffRet> Staffs
        {
            get { return _staffs; }
            set
            {
                _staffs = value;
                RaisePropertyChanged("Staffs");
            }
        }
        #endregion
        #region commands
        private ICommand _freshCommand;
        public ICommand RefreshCommand
        {
            get { return _freshCommand ?? (_freshCommand = new RelayCommand(async () => await RereshAsync(), IsFree)); }
        }


        private ICommand _deleteSelectedCommand;
        public ICommand DeleteSelectedCommand

        {
            get { return _deleteSelectedCommand ?? (_deleteSelectedCommand = new RelayCommand(async () => await RereshAsync(), IsFree)); }
        }

        
        #endregion
        private bool _isBusy = false;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                if (_isBusy) Cursor = Cursors.Wait; else Cursor = Cursors.Arrow;
            }
        }
        public bool IsFree()
        {
            return !IsBusy;
        }

        private async Task RereshAsync()
        {
            IsBusy = true;
            Staffs.Clear();
            try
            {
                var list = await App.ApiManager.BasicInfo.GetStaffs();
                Staffs = new ObservableCollection<StaffRet>(list);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
#if DEBUG
                throw ex;
#endif
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

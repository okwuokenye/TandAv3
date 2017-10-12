using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TandA.Models;
using TandA.DALs;
using TandA.ViewModels;
using System.Windows.Input;

namespace TandA.ViewModels
{
    class GroupViewModel : ObservableObject
    {

        #region Event declarations

        #endregion

        #region Private variables
        DALAdmin AdminDAL = new DALAdmin();
        DALEmployee EmployeeDAL = new DALEmployee();
        Visibility _WindowLoaderVisibility = Visibility.Collapsed;
        ObservableCollection<GroupModel> _Groups = new ObservableCollection<GroupModel>();
        GroupModel _Group;
        Boolean _IsEditGroupVisible = false;
        String _GroupRef;
        String _GroupDesc;
        String _Supervisor;
        #endregion

        #region Properties
        public string WindowTitle
        {
            get { return "Groups " + " (Time Stamp 1.0)"; }
        }

        public Visibility WindowLoaderVisibility
        {
            get { return _WindowLoaderVisibility; }
        }

        public ObservableCollection<GroupModel> Groups
        {
            get { return _Groups; }
        }

        public GroupModel Group
        {
            get { return _Group; }
            set
            {
                if (_Group != value)
                {
                    _Group = value;
                }
            }
        }

        public Visibility IsEditGroupVisible
        {
            get { return _IsEditGroupVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public String GroupRef
        {
            get { return _GroupRef; }
            set
            {
                if (_GroupRef != value)
                {
                    _GroupRef = value;
                }
            }
        }

        public String GroupDesc
        {
            get { return _GroupDesc; }
            set
            {
                if (_GroupDesc != value)
                {
                    _GroupDesc = value;
                }
            }
        }

        public String Supervisor
        {
            get { return _Supervisor; }
            set
            {
                if (_Supervisor != value)
                {
                    _Supervisor = value;
                }
            }
        }
        #endregion

        #region Constructors
        public GroupViewModel()
        {
            try
            {
                Load_Async();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".GroupViewModel\n" + ex.Message, "Error");
            }
        }

        #endregion


        #region Private functions
        async void Load_Async()
        {
            try
            {
                _WindowLoaderVisibility = Visibility.Visible;
                RaisePropertyChanged("WindowLoaderVisibility");
                await Task.Run(() =>
                {
                    _Groups = AdminDAL.GetGroups();
                });

                //Raise property changed for every property in view model
                foreach (System.Reflection.PropertyInfo p in this.GetType().GetProperties())
                {
                    RaisePropertyChanged(p.Name);
                }
                _WindowLoaderVisibility = Visibility.Collapsed;
                RaisePropertyChanged("WindowLoaderVisibility");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".Load_Async\n" + ex.Message, "Error");
            }
        }
        #endregion

        #region Public Functions

        #endregion

        #region Commands
        private async void EditGroupExecute()
        {
            try
            {
                _GroupRef = _Group.GroupRef;
                _GroupDesc = _Group.GroupDescription;
                _Supervisor = _Group.SupervisorNo;

                await Task.Run(() =>
                {
                    //get group supervisors
                });

                RaisePropertyChanged("GroupRef");
                RaisePropertyChanged("GroupDesc");
                RaisePropertyChanged("Supervisor");

                _IsEditGroupVisible = true;
                RaisePropertyChanged("IsEditGroupVisible");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".EditGroupExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand EditGroup { get { return new RelayCommand(EditGroupExecute); } }

        private void CloseEditGroupExecute()
        {
            try
            {
                _IsEditGroupVisible = false;
                RaisePropertyChanged("IsEditGroupVisible");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".CloseEditEmployeeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand CloseEditGroup { get { return new RelayCommand(CloseEditGroupExecute); } }

        private async void CreateGroupExecute()
        {
            try
            {
                _WindowLoaderVisibility = Visibility.Visible;
                RaisePropertyChanged("WindowLoaderVisibility");

                String strErr = "";
                await Task.Run(() =>
                {
                    strErr = AdminDAL.CreateGroup(_GroupRef, _GroupDesc);
                });

                if(strErr != "")
                {
                    MessageBox.Show(strErr, "Error Occured", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }else
                {
                    MessageBox.Show("Successfully created group", "Group Created", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                _GroupRef = "";
                _GroupDesc = "";

                RaisePropertyChanged("GroupRef");
                RaisePropertyChanged("GroupDesc");

                _WindowLoaderVisibility = Visibility.Collapsed;
                RaisePropertyChanged("WindowLoaderVisibility");
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".CreateGroupExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand CreateGroup { get { return new RelayCommand(CreateGroupExecute); } }
        #endregion
    }
}

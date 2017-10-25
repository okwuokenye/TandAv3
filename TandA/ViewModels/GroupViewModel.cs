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
        ObservableCollection<EmployeeModel> _Employees = new ObservableCollection<EmployeeModel>();
        GroupModel _Group;
        Boolean _IsEditGroupVisible = false;
        String _GroupRef;
        String _GroupDesc;
        String _Supervisor;
        Boolean _IsListGroup = false;
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
                    if (_IsListGroup)
                    {
                        GetEmployeesInGroup();
                    }
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

        public ObservableCollection<EmployeeModel> Employees
        {
            get { return _Employees; }
        }

        public ObservableCollection<EmployeeModel> Supervisors
        {
            get
            {
                ObservableCollection<EmployeeModel> l_Supervisors = new ObservableCollection<EmployeeModel>();
                foreach(EmployeeModel _E in _Employees.Where(m => m.MemberType == "Supervisor"))
                {
                    l_Supervisors.Add(_E);
                }
                return l_Supervisors;
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

        //overload for list employees in group
        public GroupViewModel(Boolean IsListGroup)
        {
            try
            {
                //pass bool parameter for load async overload
                Load_Async(IsListGroup);
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
                    _Employees = EmployeeDAL.GetEmployees();
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

        async void Load_Async(Boolean IsListGroup)
        {
            try
            {
                _WindowLoaderVisibility = Visibility.Visible;
                RaisePropertyChanged("WindowLoaderVisibility");
                _IsListGroup = IsListGroup;

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

        async void GetEmployeesInGroup()
        {
            try
            {
                if(_Group != null)
                {
                    _WindowLoaderVisibility = Visibility.Visible;
                    RaisePropertyChanged("WindowLoaderVisibility");
                    await Task.Run(() =>
                    {
                        _Employees = EmployeeDAL.GetEmployeesInGroup(_Group.GroupRef);
                        RaisePropertyChanged("Employees");
                        RaisePropertyChanged("Supervisors");
                    });
                    _WindowLoaderVisibility = Visibility.Collapsed;
                    RaisePropertyChanged("WindowLoaderVisibility");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".GetEmployeesInGroup\n" + ex.Message, "Error");
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

        private async void AddEmployeeToGroupExecute(object SelectedEmployees)
        {
            try
            {
                _WindowLoaderVisibility = Visibility.Visible;
                RaisePropertyChanged("WindowLoaderVisibility");

                System.Collections.IList items = (System.Collections.IList)SelectedEmployees;
                IEnumerable<EmployeeModel> l_Employees = items.Cast<EmployeeModel>();
                String err = String.Empty;

                foreach (var e in l_Employees)
                {
                    //insert into groups
                    await Task.Run(() =>
                    {
                        EmployeeDAL.AddEmployeeToGroup(e.EmployeeNumber, _Group.GroupRef);
                    });
                }

                MessageBox.Show("Successfully Added Employee(s) to group", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                _Group = null;
                await Task.Run(() =>
                {
                    _Employees = EmployeeDAL.GetEmployees();
                });

                _WindowLoaderVisibility = Visibility.Collapsed;
                RaisePropertyChanged("WindowLoaderVisibility");
                RaisePropertyChanged("Group");
                RaisePropertyChanged("Employees");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".AddEmployeeToGroupExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand AddEmployeeToGroup { get { return new RelayCommand<object>(AddEmployeeToGroupExecute); } }

        private async void CreateGroupSupervisorExecute(object SelectedEmployees)
        {
            try
            {
                _WindowLoaderVisibility = Visibility.Visible;
                RaisePropertyChanged("WindowLoaderVisibility");

                System.Collections.IList items = (System.Collections.IList)SelectedEmployees;
                IEnumerable<EmployeeModel> l_Employees = items.Cast<EmployeeModel>();
                String err = String.Empty;

                foreach (var e in l_Employees)
                {
                    //insert into groups
                    await Task.Run(() =>
                    {
                        EmployeeDAL.AddEmployeeToGroupAsSupervisor(e.EmployeeNumber, _Group.GroupRef);
                    });
                }

                MessageBox.Show("Successfully Added Supervisor(s) to group", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);

                _Group = null;
                await Task.Run(() =>
                {
                    _Employees = EmployeeDAL.GetEmployees();
                });

                _WindowLoaderVisibility = Visibility.Collapsed;
                RaisePropertyChanged("WindowLoaderVisibility");
                RaisePropertyChanged("Group");
                RaisePropertyChanged("Employees");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".CreateGroupSupervisorExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand CreateGroupSupervisor { get { return new RelayCommand<object>(CreateGroupSupervisorExecute); } }
        #endregion
    }
}

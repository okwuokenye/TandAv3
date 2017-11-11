using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TandA.DALs;
using TandA.Models;
using TandA.ViewModels;

namespace TandA.ViewModels
{
    class EmployeeViewModel : ObservableObject
    {
        #region Event declarations

        #endregion

        #region Private variables
        DALAdmin AdminDAL = new DALAdmin();
        DALEmployee EmployeeDAL = new DALEmployee();
        Visibility _WindowLoaderVisibility = Visibility.Collapsed;
        ObservableCollection<GroupModel> _Groups = new ObservableCollection<GroupModel>();
        private String _Firstname;
        private String _Lastname;
        private String _EmployeeNumber;
        private String _EmailAddress;
        private String _Password = "";
        System.Collections.IList _SelectedItems;
        ObservableCollection<EmployeeModel> _Employees = new ObservableCollection<EmployeeModel>();
        EmployeeModel _Employee;
        Visibility _IsEditEmployeeVisible = Visibility.Collapsed;
        ObservableCollection<EmployeeGroupsModel> _EmployeeGroups = new ObservableCollection<EmployeeGroupsModel>();
        #endregion

        #region Properties
        public string WindowTitle
        {
            get { return "Employees " + " (Time Stamp 1.0)"; }
        }

        public Visibility WindowLoaderVisibility
        {
            get { return _WindowLoaderVisibility; }
        }

        public ObservableCollection<GroupModel> Groups
        {
            get { return _Groups; }
        }

        public String Firstname
        {
            get
            {
                return _Firstname;
            }
            set
            {
                if (_Firstname != value)
                {
                    _Firstname = value;
                    RaisePropertyChanged("IsCreateEmployeeEnabled");
                }
            }
        }

        public String Lastname
        {
            get
            {
                return _Lastname;
            }
            set
            {
                if (_Lastname != value)
                {
                    _Lastname = value;
                    RaisePropertyChanged("IsCreateEmployeeEnabled");
                }
            }
        }

        public String EmployeeNumber
        {
            get
            {
                return _EmployeeNumber;
            }
            set
            {
                if (_EmployeeNumber != value)
                {
                    _EmployeeNumber = value;
                    RaisePropertyChanged("IsCreateEmployeeEnabled");
                }
            }
        }

        public String EmailAddress
        {
            get
            {
                return _EmailAddress;
            }
            set
            {
                if (_EmailAddress != value)
                {
                    _EmailAddress = value;
                    RaisePropertyChanged("IsCreateEmployeeEnabled");
                }
            }
        }

        public String SecurePassword
        {
            get
            {
                return _Password;
            }
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                    RaisePropertyChanged("IsCreateEmployeeEnabled");
                }
            }
        }

        public System.Collections.IList SelectedItems
        {
            get { return _SelectedItems; }
            set
            {
                if (_SelectedItems != value)
                {
                    _SelectedItems = value;
                }
            }
        }

        public ObservableCollection<EmployeeModel> Employees
        {
            get { return _Employees; }
            set
            {
                if (_Employees != value)
                {
                    _Employees = value;
                }
            }
        }

        public EmployeeModel Employee
        {
            get { return _Employee; }
            set
            {
                if (_Employee != value)
                {
                    _Employee = value;
                }
            }
        }

        public Visibility IsEditEmployeeVisible
        {
            get { return _IsEditEmployeeVisible; }
        }

        public ObservableCollection<EmployeeGroupsModel> EmployeeGroups
        {
            get { return _EmployeeGroups; }
            set
            {
                if (_EmployeeGroups != value)
                {
                    _EmployeeGroups = value;
                }
            }
        }

        public Boolean IsCreateEmployeeEnabled
        {
            get
            {
                return (_EmployeeNumber != null && _EmployeeNumber != String.Empty &&
               _Firstname != null && _Firstname != String.Empty && _Lastname != null &&
               _Lastname != String.Empty && _EmailAddress != null && _EmailAddress != String.Empty &&
               _Password != null && _Password != String.Empty);
            }
        }
        #endregion

        #region Constructors

        public EmployeeViewModel()
        {
            try
            {
                Load_Async();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".EmployeeViewModel\n" + ex.Message, "Error");
            }
        }

        public EmployeeViewModel(Boolean IsEmployeeList)
        {
            try
            {
                Load_Async(IsEmployeeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".EmployeeViewModel\n" + ex.Message, "Error");
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

        //overload for employeelist view
        async void Load_Async(Boolean IsEmployeeList)
        {
            try
            {
                _WindowLoaderVisibility = Visibility.Visible;
                RaisePropertyChanged("WindowLoaderVisibility");
                await Task.Run(() =>
                {
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
        #endregion

        #region Public Functions

        #endregion

        #region Commands
        private async void CreateEmployeeExecute(object SelectedGroups)
        {
            _WindowLoaderVisibility = Visibility.Visible;
            RaisePropertyChanged("WindowLoaderVisibility");
            try
            {
                System.Collections.IList items = (System.Collections.IList)SelectedGroups;
                var collection = items.Cast<GroupModel>();
                String err = String.Empty;

                await Task.Run(() =>
                {
                    err = EmployeeDAL.CreateEmployee(_EmployeeNumber, _Password, _Firstname, _Lastname, _EmailAddress);
                });

                if (err == String.Empty)
                {
                    foreach (var s in collection)
                    {
                        //insert into groups
                        await Task.Run(() =>
                        {
                            EmployeeDAL.AddEmployeeToGroup(_EmployeeNumber, s.GroupRef);
                        });
                    }

                    MessageBox.Show("Successfully Created Employee", "Employee Created", MessageBoxButton.OK, MessageBoxImage.Information);

                    _Groups.Clear();

                    await Task.Run(() =>
                    {
                        _Groups = AdminDAL.GetGroups();
                    });

                    _EmployeeNumber = String.Empty;
                    _Firstname = String.Empty;
                    _Lastname = String.Empty;
                    _EmailAddress = String.Empty;
                    _Password = null;

                    RaisePropertyChanged("EmployeeNumber");
                    RaisePropertyChanged("Firstname");
                    RaisePropertyChanged("Lastname");
                    RaisePropertyChanged("EmailAddress");
                    RaisePropertyChanged("SecurePassword");
                    RaisePropertyChanged("Groups");
                }
                else
                {
                    MessageBox.Show(err, "An Error Occured", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                _WindowLoaderVisibility = Visibility.Collapsed;
                RaisePropertyChanged("WindowLoaderVisibility");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".CreateEmployeeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand CreateEmployee { get { return new RelayCommand<object>(CreateEmployeeExecute); } }

        private async void EditEmployeeExecute()
        {
            try
            {
                _EmployeeNumber = _Employee.EmployeeNumber;
                _Firstname = _Employee.Firstname;
                _Lastname = _Employee.Lastname;
                _EmailAddress = _Employee.EmailAddress;
                
                await Task.Run(() =>
                {
                    _EmployeeGroups = EmployeeDAL.GetEmployeeGroups(_EmployeeNumber);
                });

                RaisePropertyChanged("EmployeeNumber");
                RaisePropertyChanged("Firstname");
                RaisePropertyChanged("Lastname");
                RaisePropertyChanged("EmailAddress");
                RaisePropertyChanged("EmployeeGroups");

                _IsEditEmployeeVisible = Visibility.Visible;
                RaisePropertyChanged("IsEditEmployeeVisible");
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".EditEmployeeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand EditEmployee {  get { return new RelayCommand(EditEmployeeExecute); } }

        private void CloseEditEmployeeExecute()
        {
            try
            {
                _IsEditEmployeeVisible = Visibility.Collapsed;
                RaisePropertyChanged("IsEditEmployeeVisible");
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".CloseEditEmployeeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand CloseEditEmployee { get { return new RelayCommand(CloseEditEmployeeExecute); } }

        private async void UpdateEmployeeExecute()
        {
            _WindowLoaderVisibility = Visibility.Visible;
            RaisePropertyChanged("WindowLoaderVisibility");
            try
            {
                
                await Task.Run(() =>
                {
                    EmployeeDAL.UpdateEmployee(_EmployeeNumber, _Password, _Firstname, _Lastname, _EmailAddress);
                });

                foreach (var s in _EmployeeGroups.Where(m => m.BelongsToGroup == true))
                {
                    //insert into groups
                    await Task.Run(() =>
                    {
                        EmployeeDAL.AddEmployeeToGroup(_EmployeeNumber, s.GroupRef);
                    });
                }

                foreach (var s in _EmployeeGroups.Where(m => m.BelongsToGroup == false))
                {
                    //remove from groups
                    await Task.Run(() =>
                    {
                        EmployeeDAL.RemoveEmployeeFromGroup(_EmployeeNumber, s.GroupRef);
                    });
                }
                MessageBox.Show("Successfully Updated Employee", "Employee Updated", MessageBoxButton.OK, MessageBoxImage.Information);

                _WindowLoaderVisibility = Visibility.Collapsed;
                RaisePropertyChanged("WindowLoaderVisibility");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".UpdateEmployeeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand UpdateEmployee
        {
            get { return new RelayCommand(UpdateEmployeeExecute); }
        }

        private async void DeleteEmployeeExecute()
        {
            _WindowLoaderVisibility = Visibility.Visible;
            RaisePropertyChanged("WindowLoaderVisibility");
            try
            {
                if(MessageBox.Show("Are you sure you want to delete employee?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _Employees.Clear();
                    await Task.Run(() =>
                    {
                        EmployeeDAL.DeleteEmployee(_EmployeeNumber);
                        _Employees = EmployeeDAL.GetEmployees();
                    });

                    _EmployeeNumber = String.Empty;
                    _Firstname = String.Empty;
                    _Lastname = String.Empty;
                    _EmailAddress = String.Empty;
                    _Password = String.Empty;

                    RaisePropertyChanged("EmployeeNumber");
                    RaisePropertyChanged("Firstname");
                    RaisePropertyChanged("Lastname");
                    RaisePropertyChanged("EmailAddress");
                    RaisePropertyChanged("EmployeeGroups");
                    RaisePropertyChanged("Password");
                    RaisePropertyChanged("Employees");

                    MessageBox.Show("Successfully Deleted Employee", "Employee Deleted", MessageBoxButton.OK, MessageBoxImage.Information);

                    _IsEditEmployeeVisible = Visibility.Collapsed;
                    RaisePropertyChanged("IsEditEmployeeVisible");
                }
                _WindowLoaderVisibility = Visibility.Collapsed;
                RaisePropertyChanged("WindowLoaderVisibility");
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".DeleteEmployeeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand DeleteEmployee {
            get { return new RelayCommand(DeleteEmployeeExecute); } }
        #endregion
    }
}

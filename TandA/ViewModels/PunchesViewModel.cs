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
    class PunchesViewModel : ObservableObject
    {
        #region Event declarations

        #endregion

        #region Private variables
        DALAdmin AdminDAL = new DALAdmin();
        DALEmployee EmployeeDAL = new DALEmployee();
        Visibility _WindowLoaderVisibility = Visibility.Collapsed;
        ObservableCollection<EmployeeModel> _Employees = new ObservableCollection<EmployeeModel>();
        EmployeeModel _Employee;
        #endregion

        #region Properties
        public string WindowTitle
        {
            get { return "Orders " + " (Time Stamp 1.0)"; }
        }

        public Visibility WindowLoaderVisibility
        {
            get { return _WindowLoaderVisibility; }
        }

        public ObservableCollection<EmployeeModel> Employees
        {
            get { return _Employees; }
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

        #endregion

        #region Constructors
        public PunchesViewModel()
        {
            try
            {
                Load_Async();
            }catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".PunchesViewModel\n" + ex.Message, "Error");
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

        #endregion
    }
}

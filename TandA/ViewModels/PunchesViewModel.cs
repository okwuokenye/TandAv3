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
        ObservableCollection<PunchesModel> _Punches = new ObservableCollection<PunchesModel>();
        PunchesModel _Punch;
        Boolean _IsViewPunchVisible = false;
        Boolean _IsAddPunchVisible = false;
        DateTime _LoginDate = DateTime.Now;
        String _LoginTime = String.Empty;
        ObservableCollection<String> _RecTypes = new ObservableCollection<String>() { "IN", "OUT"};
        String _RecType = String.Empty;
        Boolean _IsUpdatePunchVisible = false;
        PeriodModel _Period;
        String _EmployeeName = String.Empty;
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

        public ObservableCollection<PunchesModel> Punches
        {
            get { return _Punches; }
        }

        public PunchesModel Punch
        {
            get { return _Punch; }
            set
            {
                if (_Punch != value)
                {
                    _Punch = value;
                }
            }
        }

        public Visibility IsViewPunchVisible
        {
            get { return _IsViewPunchVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility IsAddPunchVisible
        {
            get { return _IsAddPunchVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public DateTime LoginDate
        {
            get { return _LoginDate.Date; }
            set
            {
                if (_LoginDate != value)
                {
                    _LoginDate = value;
                }
            }
        }

        public String LoginTime
        {
            get { return _LoginTime; }
            set
            {
                if (_LoginTime != value)
                {
                    _LoginTime = value;
                }
            }
        }

        public ObservableCollection<String> RecTypes
        {
            get { return _RecTypes; }
        }

        public String RecType
        {
            get { return _RecType; }
            set
            {
                if (_RecType != value)
                {
                    _RecType = value;
                }
            }
        }

        public String PeriodText
        {
            get { return "Period: " + (_Period != null ? ( _Period.StartDate.Date.ToShortDateString() + " - " + _Period.EndDate.Date.ToShortDateString()) : "n/a"); }
        }
        public Visibility IsUpdatePunchVisible
        {
            get { return _IsUpdatePunchVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public String EmployeeName
        {
            get { return _Employee != null ? _Employee.Firstname + " " + _Employee.Lastname : "n/a"; }
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
                    _Period = AdminDAL.GetCurrentPeriodDetail();
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
        private async void ViewPunchesExecute()
        {
            try
            {
                await Task.Run(() =>
                {
                    //get the punches for the stipulated period and employee
                    _Punches = AdminDAL.GetEmployeePunches(_Employee.EmployeeNumber, _Period.ID);
                });
                _IsViewPunchVisible = true;
                RaisePropertyChanged("EmployeeName");
                RaisePropertyChanged("IsViewPunchVisible");
                RaisePropertyChanged("Punches");
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".ViewPunchesExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand ViewPunches { get { return new RelayCommand(ViewPunchesExecute); } }

        private void CloseViewPunchesExecute()
        {
            _IsViewPunchVisible = false;
            RaisePropertyChanged("IsViewPunchVisible");
        }
        public ICommand CloseViewPunches { get { return new RelayCommand(CloseViewPunchesExecute); } }

        private void AddNewPunchExecute()
        {
            _IsAddPunchVisible = true;
            RaisePropertyChanged("IsAddPunchVisible");
        }
        public ICommand AddNewPunch { get { return new RelayCommand(AddNewPunchExecute); } }

        private async void AddPunchExecute()
        {
            try
            {
                _Punches.Clear();
                await Task.Run(() =>
                {
                    AdminDAL.CreateEmployeePunch(_Employee.EmployeeNumber, _LoginDate, _LoginTime, _Period.ID, _RecType);
                    _Punches = AdminDAL.GetEmployeePunches(_Employee.EmployeeNumber, _Period.ID);
                });

                MessageBox.Show("Successfully added employee punch", "Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                _IsAddPunchVisible = false;
                _LoginDate = DateTime.Now;
                _LoginTime = "";
                _RecType = null;
                RaisePropertyChanged("IsAddPunchVisible");
                RaisePropertyChanged("LoginDate");
                RaisePropertyChanged("LoginTime");
                RaisePropertyChanged("RecType");
                RaisePropertyChanged("Punches");
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".AddPunchExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand AddPunch { get { return new RelayCommand(AddPunchExecute); } }

        private void CloseAddNewPunchExecute()
        {
            _IsAddPunchVisible = false;
            _LoginDate = DateTime.Now;
            _LoginTime = "";
            _RecType = null;

            RaisePropertyChanged("IsAddPunchVisible");
            RaisePropertyChanged("LoginDate");
            RaisePropertyChanged("LoginTime");
            RaisePropertyChanged("RecType");
        }
        public ICommand CloseAddNewPunch { get { return new RelayCommand(CloseAddNewPunchExecute); } }

        private void EditPunchExecute()
        {
            _IsUpdatePunchVisible = true;
            _LoginDate = _Punch.PunchDate;
            _LoginTime = _Punch.PunchTime;
            _RecType = _Punch.PunchType;

            RaisePropertyChanged("IsUpdatePunchVisible");
            RaisePropertyChanged("LoginDate");
            RaisePropertyChanged("LoginTime");
            RaisePropertyChanged("RecType");
        }
        public ICommand EditPunch { get { return new RelayCommand(EditPunchExecute); } }

        private void CloseUpdatePunchExecute()
        {
            _IsUpdatePunchVisible = false;
            _LoginDate = DateTime.Now;
            _LoginTime = String.Empty;
            _RecType = String.Empty;

            RaisePropertyChanged("IsUpdatePunchVisible");
            RaisePropertyChanged("LoginDate");
            RaisePropertyChanged("LoginTime");
            RaisePropertyChanged("RecType");
        }
        public ICommand CloseUpdatePunch { get { return new RelayCommand(CloseUpdatePunchExecute); } }

        private async void UpdatePunchExecute()
        {
            try
            {
                await Task.Run(() =>
                {

                });
            }catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".UpdatePunchExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand UpdatePunch { get { return new RelayCommand(UpdatePunchExecute); } }
        #endregion
    }
}


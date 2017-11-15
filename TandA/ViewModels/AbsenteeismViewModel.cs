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
    class AbsenteeismViewModel : ObservableObject
    {
        #region Event declarations

        #endregion

        #region Private variables
        DALAdmin AdminDAL = new DALAdmin();
        DALEmployee EmployeeDAL = new DALEmployee();
        Visibility _WindowLoaderVisibility = Visibility.Collapsed;
        ObservableCollection<AbsenteeismModel> _ACodes = new ObservableCollection<AbsenteeismModel>();
        AbsenteeismModel _ACode;
        
        String _Reference;
        String _Description;
        String _Abbreviation;
        Boolean _IsEditACodeVisible = false;

        ObservableCollection<EmployeeAbsenteeismModel> _Absents = new ObservableCollection<EmployeeAbsenteeismModel>();
        EmployeeAbsenteeismModel _Absent;

        Boolean _IsListView = false;
        Boolean _IsCreateEmployeeAbsenteeismVisible = false;
        ObservableCollection<EmployeeModel> _Employees = new ObservableCollection<EmployeeModel>();
        EmployeeModel _Employee;

        DateTime _DateAbsent = DateTime.Now;
        DateTime _DateReturned = DateTime.Now;
        String _Note;
        Boolean _IsEditEmployeeAbsenteeismVisible = false;
        Int32 _Id;
        Boolean _IsPaid = true;
        String _TImeAbsent = "06:00";
        String _TImeReturned = "14:30";
        #endregion

        #region Properties
        public string WindowTitle
        {
            get { return "Absenteeism " + " (Time Stamp 1.0)"; }
        }

        public Visibility WindowLoaderVisibility
        {
            get { return _WindowLoaderVisibility; }
        }

        public ObservableCollection<AbsenteeismModel> ACodes
        {
            get { return _ACodes; }
        }

        public AbsenteeismModel ACode
        {
            get { return _ACode; }
            set
            {
                if(_ACode != value)
                {
                    _ACode = value;
                }
            }
        }

        public String Reference
        {
            get { return _Reference; }
            set
            {
                if(_Reference != value)
                {
                    _Reference = value;
                }
            }
        }

        public String Description
        {
            get { return _Description; }
            set
            {
                if(_Description != value)
                {
                    _Description = value;
                }
            }
        }

        public String Abbreviation
        {
            get { return _Abbreviation; }
            set
            {
                if(_Abbreviation != value)
                {
                    _Abbreviation = value;
                }
            }
        }

        public Visibility IsEditACodeVisible
        {
            get { return _IsEditACodeVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility IsCreateEmployeeAbsenteeismVisible
        {
            get { return _IsCreateEmployeeAbsenteeismVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public ObservableCollection<EmployeeAbsenteeismModel> Absents
        {
            get { return _Absents; }
        }

        public EmployeeAbsenteeismModel Absent
        {
            get { return _Absent; }
            set
            {
                if (_Absent != value)
                {
                    _Absent = value;
                }
            }
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

        public DateTime DateAbsent
        {
            get { return _DateAbsent; }
            set
            {
                if (_DateAbsent != value)
                {
                    _DateAbsent = value;
                }
            }
        }
        
        public DateTime DateReturned
        {
            get { return _DateReturned; }
            set
            {
                if (_DateReturned != value)
                {
                    _DateReturned = value;
                }
            }
        }

        public String Note
        {
            get { return _Note; }
            set
            {
                if (_Note != value)
                {
                    _Note = value;
                }
            }
        }

        public Visibility IsEditEmployeeAbsenteeismVisible
        {
            get { return _IsEditEmployeeAbsenteeismVisible ? Visibility.Visible : Visibility.Collapsed;  }
        }

        public Boolean IsPaid
        {
            get { return _IsPaid; }
            set
            {
                if (_IsPaid != value)
                {
                    _IsPaid = value;
                }
            }
        }

        public String TimeAbsent
        {
            get { return _TImeAbsent; }
            set
            {
                if (_TImeAbsent != value)
                {
                    _TImeAbsent = value;
                }
            }
        }

        public String TimeReturned
        {
            get { return _TImeReturned; }
            set
            {
                if (_TImeReturned != value)
                {
                    _TImeReturned = value;
                }
            }
        }
        #endregion

        #region Constructors
        public AbsenteeismViewModel()
        {
            try
            {
                Load_Async();
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".AbsenteeismViewModel\n" + ex.Message, "Error");
            }
        }

        public AbsenteeismViewModel(Boolean IsListView)
        {
            try
            {
                _IsListView = IsListView;
                Load_Async();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".AbsenteeismViewModel\n" + ex.Message, "Error");
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
                    _ACodes = AdminDAL.GetACodes();
                });
                //check if mood is to view employee absenteeism or absenteeism settings
                if (_IsListView)
                {
                    await Task.Run(() =>
                    {
                        //populate absents
                        _Absents = AdminDAL.GetEmployeeAbsenteeism();
                        _Employees = EmployeeDAL.GetEmployees();
                    });
                }
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
        
        async void Refresh()
        {
            try
            {
                _ACodes.Clear();

                await Task.Run(() =>
                {
                    _ACodes = AdminDAL.GetACodes();
                });
            }catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".Refresh\n" + ex.Message, "Error");
            }
        }
        #endregion

        #region Public Functions

        #endregion

        #region Commands
        private void EditACodeExecute()
        {
            try
            {
                _IsEditACodeVisible = true;
                _Reference = _ACode.Reference;
                _Description = _ACode.Description;
                _Abbreviation = _ACode.Abbreviation;

                RaisePropertyChanged("Reference");
                RaisePropertyChanged("Description");
                RaisePropertyChanged("Abbreviation");
                RaisePropertyChanged("IsEditACodeVisible");
            }catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".EditACodeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand EditACode { get { return new RelayCommand(EditACodeExecute); } }

        private void CloseEditACodeExecute()
        {
            try
            {
                _IsEditACodeVisible = false;
                RaisePropertyChanged("IsEditACodeVisible");
                RaisePropertyChanged("ACodes");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".CloseEditACodeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand CloseEditACode { get { return new RelayCommand(CloseEditACodeExecute); } }

        private async void DeleteACodeExecute()
        {
            try
            {
                MessageBoxResult l_response = MessageBox.Show("Are you sure you want to delete this Absenteeism code?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(l_response == MessageBoxResult.Yes)
                {
                    //call delete dal method
                    await Task.Run(() =>
                    {
                        AdminDAL.DeleteACode(_Reference);
                    });
                    MessageBox.Show("Absenteeism code deleted", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                    Refresh();

                    _IsEditACodeVisible = false;
                    RaisePropertyChanged("IsEditACodeVisible");
                    RaisePropertyChanged("ACodes");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".DeleteACodeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand DeleteACode {  get { return new RelayCommand(DeleteACodeExecute); } }

        private async void UpdateACodeExecute()
        {
            try
            {
                await Task.Run(() =>
                {
                    AdminDAL.UpdateACode(_Reference, _Description, _Abbreviation);
                });
                MessageBox.Show("Absenteeism code updated", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                Refresh();
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".UpdateACodeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand UpdateACode { get { return new RelayCommand(UpdateACodeExecute); } }

        private async void CreateACodeExecute()
        {
            try
            {
                await Task.Run(() =>
                {
                    AdminDAL.CreateACode(_Reference, _Description, _Abbreviation);
                });

                MessageBox.Show("Successfully created Absenteeism Code", "Created", MessageBoxButton.OK, MessageBoxImage.Information);

                _Reference = "";
                _Description = "";
                _Abbreviation = "";

                RaisePropertyChanged("Reference");
                RaisePropertyChanged("Description");
                RaisePropertyChanged("Abbreviation");
            }catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".CreateACodeExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand CreateACode { get { return new RelayCommand(CreateACodeExecute); } }

        private void AddNewAbsenteeismExecute()
        {
            _IsCreateEmployeeAbsenteeismVisible = true;
            RaisePropertyChanged("IsCreateEmployeeAbsenteeismVisible");
        }
        public ICommand AddNewAbsenteeism { get { return new RelayCommand(AddNewAbsenteeismExecute); } }

        private async void CreateEmployeeAbsenteeismExecute()
        {
            try
            {
                _WindowLoaderVisibility = Visibility.Visible;
                RaisePropertyChanged("WindowLoaderVisibility");

                await Task.Run(() =>
                {
                    AdminDAL.CreateEmployeeAbsenteeism(_Employee.EmployeeNumber, _DateAbsent, _TImeAbsent, _DateReturned, _TImeReturned, _ACode.Reference, _IsPaid, _Note);
                });
                _Absents.Clear();

                await Task.Run(() =>
                {
                    //populate absents
                    _Absents = AdminDAL.GetEmployeeAbsenteeism();
                });

                _IsCreateEmployeeAbsenteeismVisible = false;
                _Employee = null;
                _DateAbsent = DateTime.Now;
                _DateReturned = DateTime.Now;
                _ACode = null;
                _Note = null;
                _IsPaid = false;

                RaisePropertyChanged("Absents");
                RaisePropertyChanged("Employee");
                RaisePropertyChanged("DateAbsent");
                RaisePropertyChanged("DateReturned");
                RaisePropertyChanged("ACode");
                RaisePropertyChanged("IsPaid");
                RaisePropertyChanged("Note");
                RaisePropertyChanged("IsCreateEmployeeAbsenteeismVisible");

                _WindowLoaderVisibility = Visibility.Collapsed;
                RaisePropertyChanged("WindowLoaderVisibility");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".CreateEmployeeAbsenteeismExecute\n" + ex.Message, "Error");
            }
        }
        private Boolean CanCreateEmployeeAbsenteeism()
        {
            return (_Employee != null && _Employee.EmployeeNumber != null && _DateAbsent != null && _DateReturned != null && _ACode != null && _ACode.Reference != null && _Note != null);
        }
        public ICommand CreateEmployeeAbsenteeism { get { return new RelayCommand(CreateEmployeeAbsenteeismExecute, CanCreateEmployeeAbsenteeism); } }

        private void CloseCreateEmployeeAbsenteeismExecute()
        {
            _IsCreateEmployeeAbsenteeismVisible = false;
            RaisePropertyChanged("IsCreateEmployeeAbsenteeismVisible");
        }
        public ICommand CloseCreateEmployeeAbsenteeism { get { return new RelayCommand(CloseCreateEmployeeAbsenteeismExecute); } }

        private void EditAbsenteeismExecute()
        {
            try
            {
                _IsEditEmployeeAbsenteeismVisible = true;

                _Employee = _Employees.SingleOrDefault(m => m.EmployeeNumber == _Absent.EmployeeReference);
                _DateAbsent = _Absent.DateAbsent;
                _DateReturned = _Absent.TimeTo;
                _ACode = _ACodes.SingleOrDefault(m => m.Reference == _Absent.AbsentRef);
                _Note = _Absent.Note;
                _Id = _Absent.Id;
                _IsPaid = _Absent.IsPaid;
                _TImeAbsent = _DateAbsent.TimeOfDay.ToString();
                _TImeReturned = _DateReturned.TimeOfDay.ToString();

                RaisePropertyChanged("Absents");
                RaisePropertyChanged("Employee");
                RaisePropertyChanged("DateAbsent");
                RaisePropertyChanged("DateReturned");
                RaisePropertyChanged("ACode");
                RaisePropertyChanged("IsPaid");
                RaisePropertyChanged("Note");
                RaisePropertyChanged("TimeAbsent");
                RaisePropertyChanged("TimeReturned");
                RaisePropertyChanged("IsEditEmployeeAbsenteeismVisible");
            }
            catch(Exception ex)
            {
                MessageBox.Show(this.ToString() + ".EditAbsenteeismExecute\n" + ex.Message, "Error");
            }
        }
        private Boolean CanEditAbseenteeism()
        {
            return _Absent != null;
        }
        public ICommand EditAbsenteeism { get { return new RelayCommand(EditAbsenteeismExecute, CanEditAbseenteeism); } }

        private void CancelEditAbsenteeismExecute()
        {
            try
            {
                _IsEditEmployeeAbsenteeismVisible = false;

                _Employee = null;
                _DateAbsent = DateTime.Now;
                _DateReturned = DateTime.Now;
                _ACode = null;
                _Note = null;
                _IsPaid = false;

                RaisePropertyChanged("Absents");
                RaisePropertyChanged("Employee");
                RaisePropertyChanged("DateAbsent");
                RaisePropertyChanged("DateReturned");
                RaisePropertyChanged("IsPaid");
                RaisePropertyChanged("ACode");
                RaisePropertyChanged("Note");
                RaisePropertyChanged("IsEditEmployeeAbsenteeismVisible");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".EditAbsenteeismExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand CancelEditAbsenteeism { get { return new RelayCommand(CancelEditAbsenteeismExecute); } }

        private async void UpdateEmployeeAbsenteeismExecute()
        {
            try
            {
                _WindowLoaderVisibility = Visibility.Visible;
                RaisePropertyChanged("WindowLoaderVisibility");

                _Absents.Clear();
                await Task.Run(() =>
                {
                    AdminDAL.UpdateEmployeeAbsenteeism(_Id, _Employee.EmployeeNumber, _DateAbsent, _TImeAbsent, _DateReturned, _TImeReturned, _ACode.Reference, _IsPaid, _Note);
                    _Absents = AdminDAL.GetEmployeeAbsenteeism();
                });

                MessageBox.Show("Successfully updated Employee absenteeism", "Update Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                _WindowLoaderVisibility = Visibility.Collapsed;

                RaisePropertyChanged("Absents");
                RaisePropertyChanged("WindowLoaderVisibility");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".UpdateEmployeeAbsenteeismExecute\n" + ex.Message, "Error");
            }
        }
        public ICommand UpdateEmployeeAbsenteeism { get { return new RelayCommand(UpdateEmployeeAbsenteeismExecute, CanCreateEmployeeAbsenteeism); } }
        #endregion
    }
}

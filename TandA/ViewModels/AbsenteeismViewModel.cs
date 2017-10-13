﻿using System;
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
        #endregion
    }
}

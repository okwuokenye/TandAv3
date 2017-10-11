using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TandA.ViewModels
{
    class StartScreenViewModel : ObservableObject
    {
        #region Event declarations

        #endregion

        #region Private variables

        Visibility _WindowLoaderVisibility = Visibility.Collapsed;
        #endregion

        #region Properties
        public string WindowTitle
        {
            get { return "Start Screen " + " (Time Stamp 1.0)"; }
        }

        public Visibility WindowLoaderVisibility
        {
            get { return _WindowLoaderVisibility; }
        }

        #endregion

        #region Constructors
        public StartScreenViewModel()
        {
            try
            {
                Load_Async();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.ToString() + ".StartScreenViewModel\n" + ex.Message, "Error");
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
        private void AddEMployeeExecute()
        {

        }
        #endregion
    }
}

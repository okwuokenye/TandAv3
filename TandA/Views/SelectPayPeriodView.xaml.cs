using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TandA.ViewModels;

namespace TandA.Views
{
    /// <summary>
    /// Interaction logic for SelectPayPeriodView.xaml
    /// </summary>
    public partial class SelectPayPeriodView : Window
    {
        HRReportViewModel vm;
        HRReportView view;
        public SelectPayPeriodView()
        {
            InitializeComponent();
            vm = new HRReportViewModel();
            base.DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            view = new HRReportView(vm.Period);
            view.ShowDialog();
        }
    }
}

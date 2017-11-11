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
using TandA.Models;

namespace TandA.Views
{
    /// <summary>
    /// Interaction logic for HRReportView.xaml
    /// </summary>
    public partial class HRReportView : Window
    {
        HRReportViewModel vm;
        public HRReportView()
        {
            InitializeComponent();
            vm = new HRReportViewModel();
            base.DataContext = vm;
            FillTable();
            //HRR_DataGrid.DataContext = vm.HRR.DefaultView;
        }

        private void FillTable()
        {
            vm.FillTable();
        }

        public HRReportView(PeriodModel p_PeriodStart)
        {
            InitializeComponent();
            vm = new HRReportViewModel();
            vm.Period = p_PeriodStart;

            base.DataContext = vm;
            FillTable();
            //HRR_DataGrid.DataContext = vm.HRR.DefaultView;
        }
    }
}

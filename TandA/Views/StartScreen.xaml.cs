using Syncfusion.Windows.Tools.Controls;
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
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : RibbonWindow
    {
        StartScreenViewModel vm;
        public StartScreen()
        {
            InitializeComponent();
            vm = new StartScreenViewModel();
            base.DataContext = vm;
        }
        
        private void Add_Employee_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployeeView cE = new CreateEmployeeView();
            cE.Show();
        }

        private void Employee_List_Click(object sender, RoutedEventArgs e)
        {
            EmployeeListVIew EL = new EmployeeListVIew();
            EL.Show();
        }

        private void Add_Group_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployeeView cE = new CreateEmployeeView();
            cE.Show();
        }

        private void Group_List_Click(object sender, RoutedEventArgs e)
        {
            GroupsListView GL = new GroupsListView();
            GL.Show();
        }
    }
}

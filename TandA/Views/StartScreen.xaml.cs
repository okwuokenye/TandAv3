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
            CreateGroupView cG = new CreateGroupView();
            cG.Show();
        }

        private void Group_List_Click(object sender, RoutedEventArgs e)
        {
            GroupsListView GL = new GroupsListView();
            GL.Show();
        }

        private void Add_Absenthiesm_Click(object sender, RoutedEventArgs e)
        {
            CreateAbsenteeismView cA = new CreateAbsenteeismView();
            cA.Show();
        }

        private void Absenthiesm_List_Click(object sender, RoutedEventArgs e)
        {
            AbsentListView AL = new AbsentListView();
            AL.Show();
        }

        private void Add_GroupMember_Click(object sender, RoutedEventArgs e)
        {
            AddGroupMemberView cG = new AddGroupMemberView();
            cG.Show();
        }

        private void View_GroupMembers_Click(object sender, RoutedEventArgs e)
        {
            GroupEmployeesView GV = new GroupEmployeesView();
            GV.Show();
        }
        
        private void Add_Supervisor_Click(object sender, RoutedEventArgs e)
        {
            SetGroupSupervisorView GS = new SetGroupSupervisorView();
            GS.Show();
        }

        private void View_Supervisors_Click(object sender, RoutedEventArgs e)
        {
            GroupSupervisorsView GV = new GroupSupervisorsView();
            GV.Show();
        }

        private void View_Punches_Click(object sender, RoutedEventArgs e)
        {
            PunchesListView PV = new PunchesListView();
            PV.Show();
        }

        private void View_HRR_Click(object sender, RoutedEventArgs e)
        {
            SelectPayPeriodView _HRR = new SelectPayPeriodView();
            _HRR.Show();
        }

        private void View_Absenteeism_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAbsenteeismListView EAL = new EmployeeAbsenteeismListView();
            EAL.Show();
        }

        private void AddNew_Absenteeism_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

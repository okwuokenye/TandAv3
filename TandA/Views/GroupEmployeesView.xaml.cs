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
    /// Interaction logic for ViewGroupEmployees.xaml
    /// </summary>
    public partial class GroupEmployeesView : Window
    {
        GroupViewModel vm;
        public GroupEmployeesView()
        {
            InitializeComponent();
            vm = new GroupViewModel(true);
            base.DataContext = vm;
        }

        private void Add_GroupMember_Click(object sender, RoutedEventArgs e)
        {
            AddGroupMemberView cG = new AddGroupMemberView();
            cG.Show();
        }
    }
}

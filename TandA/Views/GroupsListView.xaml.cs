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
    /// Interaction logic for GroupsListView.xaml
    /// </summary>
    public partial class GroupsListView : Window
    {
        GroupViewModel vm;
        public GroupsListView()
        {
            InitializeComponent();
            vm = new GroupViewModel();
            base.DataContext = vm;
        }
    }
}

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
    /// Interaction logic for CreateGroupView.xaml
    /// </summary>
    public partial class CreateGroupView : Window
    {
        GroupViewModel vm;

        public CreateGroupView()
        {
            InitializeComponent();
            vm = new GroupViewModel();
            base.DataContext = vm;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

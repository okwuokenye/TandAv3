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
    /// Interaction logic for PunchesListView.xaml
    /// </summary>
    public partial class PunchesListView : Window
    {
        PunchesViewModel vm;
        public PunchesListView()
        {
            InitializeComponent();
            vm = new PunchesViewModel();
            base.DataContext = vm;
        }
    }
}

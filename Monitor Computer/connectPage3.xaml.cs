using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monitor_Computer
{
    /// <summary>
    /// Interaction logic for connectPage3.xaml
    /// </summary>
    public partial class connectPage3 : Page
    {
        private MainWindow mainWindowClass;
        public connectPage3(MainWindow mainWindowClass)
        {
            InitializeComponent();
            this.mainWindowClass = mainWindowClass;
        }
    }
}

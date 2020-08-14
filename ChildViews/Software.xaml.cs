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

namespace CNNBasedPHM.ChildViews
{
    /// <summary>
    /// Software.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Software : UserControl
    {
        public Software()
        {
            InitializeComponent();
        }

        private void xml_toConsole_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Page_Monitor.staticMoveLeft();

            Task.Run(() => 
            {
                Thread.Sleep(2000);
                this.Dispatcher.Invoke(() =>
                {
                    this.Visibility = Visibility.Collapsed;
                });
            });

            
        }
    }
}

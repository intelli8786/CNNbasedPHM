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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNNBasedPHM.PublicControl
{
    /// <summary>
    /// ClassPercentage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ClassPercentage : UserControl
    {
        public ClassPercentage()
        {
            InitializeComponent();
        }


        public ImageSource ClassImage
        {
            get
            {
                return xml_ThumbNail.Source;
            }
            set
            {
                xml_ThumbNail.Source = value;
            }
        }


        public String ClassName
        {
            get
            {
                return xml_ClassName.Text;
            }
            set
            {
                xml_ClassName.Text = value;
            }
        }

        public Double Probability
        {
            get
            {
                return Double.Parse(xml_ProbabilityText.Text);
            }
            set
            {
                xml_ProbabilityText.Text = ((int)value).ToString() + "%";
                xml_ProbabilityView.BeginAnimation(Border.HeightProperty, new DoubleAnimation(120 * value / 100, new Duration(TimeSpan.FromMilliseconds(300))));
            }
        }
    }
}

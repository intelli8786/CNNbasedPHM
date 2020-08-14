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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CNNBasedPHM.PublicControl
{
    /// <summary>
    /// ProgressImage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ProgressImage : UserControl
    {
        public ProgressImage()
        {
            InitializeComponent();
        }

        public ImageSource ElementImage
        {
            get
            {
                return xml_Element.Source;
            }
            set
            {
                xml_Element.Source = value;
            }
        }

        public String ElementName
        {
            get
            {
                return xml_ElementName.Text;
            }
            set
            {
                xml_ElementName.Text = value;
            }
        }
    }
}

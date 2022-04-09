using ModbusInfoPart.Data;
using ModbusPart.ViewModel;
using System.Windows.Controls;

namespace ModbusPart.Sub
{
    /// <summary>
    /// Interaction logic for UCTCP.xaml
    /// </summary>
    public partial class UCModbusTCP : UserControl
    {
        internal TCPViewModel viewModel = new TCPViewModel();


        public UCModbusTCP()
        {
            InitializeComponent();
            this.DataContext = viewModel;
          
        }


    }
}

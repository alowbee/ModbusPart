using ModbusInfoPart.Data;
using ModbusPart.ViewModel;
using System.Windows.Controls;


namespace ModbusPart.Sub
{
    /// <summary>
    /// Interaction logic for UCSerial.xaml
    /// </summary>
    public partial class UCModbusSerial : UserControl
    {
        internal SerialViewModel viewModel = new SerialViewModel();
        public UCModbusSerial()
        {
            InitializeComponent();
            this.DataContext = viewModel;
 
        }


    }
}

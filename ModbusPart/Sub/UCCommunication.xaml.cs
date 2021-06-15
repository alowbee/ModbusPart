using ModbusInfoPart.Data;
using ModbusPart.ViewModel;
using System.Windows.Controls;

namespace ModbusPart.Sub
{
    /// <summary>
    /// Interaction logic for UCIMPTCP.xaml
    /// </summary>
    public partial class UCCommunication : UserControl
    {
        public CommunicationViewModel viewModel = new CommunicationViewModel();
        public UCCommunication()
        {
            InitializeComponent();
            this.DataContext = viewModel;
          
        }
    }
}

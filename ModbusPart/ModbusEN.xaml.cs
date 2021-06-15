using ModbusInfoPart.Data;
using ModbusPart.Sub;
using System.Windows;
using System.Windows.Controls;
using WPFToolkit.Controls;

namespace ModbusPart
{
    /// <summary>
    /// Interaction logic for ModbusEN.xaml
    /// </summary>
    public partial class ModbusEN : CustomWindow
    {
        public ModbusEN()
        {
            InitializeComponent();
        }





        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {

            if (UCModbus.MainViewModel.SubContent is UCDevice devicecontent)
            {
                var item = devicecontent.viewModel.CurrentItem;
                if (item != null)
                {
                    if (item.IsComplete == true)
                    {
                        DataHelper.SetReadMap();
                        DataHelper.SetWriteMap();
                        UCModbus.FileSaveTrg = true;
                        //DataHelper.SaveFile();
                    }
                }
            }
            this.Close();
        }



        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (UCModbus.MainViewModel.SubContent is UCDevice devicecontent)
            {
                var item = devicecontent.viewModel.CurrentItem;
                  if (devicecontent.viewModel.CurrentItem != null)
                {
                    var selecteditem = EnableComboBox.SelectedItem as ComboBoxItem;
                    if (selecteditem == MRegItem)
                    { item.ButtonContent = "M" + MValue.Value; }
                    else
                    {
                        item.ButtonContent = selecteditem.Content.ToString();
                    }
                    if (item.IsComplete == true)
                    {
                        DataHelper.SetReadMap();
                        DataHelper.SetWriteMap();
                        //DataHelper.SaveFile();
                        UCModbus.FileSaveTrg = true;
                    }
                }


                this.Close();
            }
        }


    }
}

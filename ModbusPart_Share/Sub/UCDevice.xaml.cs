using CNC_SNC_CSharp;
using ModbusInfoPart.Data;
using ModbusPart.Data;
using ModbusPart.ViewModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace ModbusPart.Sub
{
    /// <summary>
    /// Interaction logic for UCDevice.xaml
    /// </summary>
    public partial class UCDevice : UserControl
    {
        public DeviceViewModel viewModel = new DeviceViewModel();
        public UCDevice()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        
        }

        /// <summary>
        /// 十六進制鍵盤限制
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HEXKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key < Key.D0 | e.Key > Key.D9))
            {
                if (e.Key < Key.NumPad0 | e.Key > Key.NumPad9)
                {
                    if (e.Key < Key.A | e.Key > Key.F)
                    {
                        if (e.Key != Key.Back)
                        {
                            e.Handled = true;
                        }

                    }

                }
            }
        }
        /// <summary>
        /// 整數數字鍵盤輸入限制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((e.Key < Key.D0 | e.Key > Key.D9))
            {
                if (e.Key < Key.NumPad0 | e.Key > Key.NumPad9)
                {
                    if (e.Key != Key.Back)
                    {
                        e.Handled = true;
                    }
                }
            }
        }



        /// <summary>
        /// ReadMap_Func修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadFunc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ButtonContent设置
            var cb = (e.Source as ComboBox).SelectionBoxItem;
            if (string.IsNullOrEmpty(cb.ToString()))
            {
                if (viewModel.CurrentItem != null)
                {
                    viewModel.CurrentItem.ButtonContent = Properties.Lang.Lang.Enable;
                    viewModel.CurrentItem.Length = 1;
                }
            }

            if (this.viewModel.ReadMap.Where(v => v.IsComplete == false).Count() == 0)
            {
                this.viewModel.ReadMap.Add(new MapItem() { Index = this.viewModel.ReadMap.Count + 1 });      
            }
            DataHelper.SetReadMap();
            UCModbus.FileSaveTrg = true;

        }
        /// <summary>
        /// ReadMap編輯單元格完成時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReadDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (this.viewModel.ReadMap.Where(v => v.IsComplete == false).Count() == 0)
            {
                this.viewModel.ReadMap.Add(new MapItem() { Index = this.viewModel.ReadMap.Count + 1 });
            }
            DataHelper.SetReadMap();
            UCModbus.FileSaveTrg = true;

        }



        /// <summary>
        /// WriteMap編輯單元格完成時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteMapDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (this.viewModel.WriteMap.Where(v => v.IsComplete == false).Count() == 0)
            {
                this.viewModel.WriteMap.Add(new MapItem() { Index = this.viewModel.WriteMap.Count + 1 });
            }
            DataHelper.SetWriteMap();
            UCModbus.FileSaveTrg = true;

        }


        /// <summary>
        /// WriteMap_Func修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WriteFunc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ButtonContent设置
            var cb = (e.Source as ComboBox).SelectionBoxItem;
            if (string.IsNullOrEmpty(cb.ToString()))
            {
                if (viewModel.CurrentItem != null)
                {
                    viewModel.CurrentItem.ButtonContent = Properties.Lang.Lang.Enable;
                    viewModel.CurrentItem.Length = 1;
                }
            }
            if (this.viewModel.WriteMap.Where(v => v.IsComplete == false).Count() == 0)
            {
                this.viewModel.WriteMap.Add(new MapItem() { Index = this.viewModel.WriteMap.Count + 1 });
            }
            DataHelper.SetWriteMap();
            UCModbus.FileSaveTrg = true;
        }
    }
}
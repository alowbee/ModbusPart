using CNC_SNC_CSharp;
using ModbusInfoPart.Data;
using ModbusPart.Data;
using ModbusPart.Sub;
using ModbusPart.ViewModel;
using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ModbusPart
{
    /// <summary>
    /// Interaction logic for UCModbus.xaml
    /// </summary>
    public partial class UCModbus : UserControl
    {

        /// <summary>
        /// 主页面对接数据VM
        /// </summary>
        internal static ModusViewModel MainViewModel = new ModusViewModel();
        // private DispatcherTimer timer = new DispatcherTimer();
        private System.Timers.Timer timer = new System.Timers.Timer(1000);
        internal static bool FileSaveTrg = false;
        public UCModbus()
        {
            InitializeComponent();
            LoadDefaultContent();
            this.DataContext = MainViewModel;
            this.Unloaded += UCModbus_Unloaded;
            this.Loaded += (o,e)=> { timer.Start(); };
            // timer.Tick += Timer_Tick;
             //1秒一个循环
            timer.Elapsed += timer_Elapsed;

        }

        private void timer_Elapsed(object sender, EventArgs e)
        {
            if (FileSaveTrg)
            {
                FileSaveTrg = false;
                Dispatcher.BeginInvoke(new Action(DataHelper.SaveFile),null);
                if (ShareMem_main.ShareMemIsOpened)
                    ShareMem_main.CS_Set_W(9010, 1020, false);//IMP reload setting 
                //MessageBox.Show("Saved");
            }
        }



        /// <summary>
        /// 界面消失需要保存数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UCModbus_Unloaded(object sender, RoutedEventArgs e)
        {
            DataHelper.SaveFile();
            // timer.IsEnabled = false;
            timer.Stop();
            if (ShareMem_main.ShareMemIsOpened)
                ShareMem_main.CS_Set_W(9010, 1020, false);//Reload Config.ini
        }



        /// <summary>
        /// 默认初始选中ModusTCP界面
        /// </summary>
        private void LoadDefaultContent()
        {
            var communicationcontent = new UCCommunication();
            communicationcontent.viewModel.Type = SlaveType.TCP;
            PageVMHelper.LoadCommunicationContent(communicationcontent);
            MainViewModel.TCPMainNode.IsSelected = true;
            MainViewModel.SubContent = communicationcontent;
            MainViewModel.Pagetitle = MainViewModel.TCPMainNode.Name;
        }

        /// <summary>
        /// Node节点选择切换页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = e.OriginalSource as TreeViewItem;
            var model = tvi.Header as TreeViewNode;
            if (model != null)
            {
                MainViewModel.CurrentNode = model;
                if (MainViewModel.SubContent is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                switch (model.NodeType)
                {
                    case NodeType.IMP:
                        break;
                    case NodeType.TCP:
                        var tcpcommunication = new UCCommunication();
                        tcpcommunication.viewModel.Type = SlaveType.TCP;
                        PageVMHelper.LoadCommunicationContent(tcpcommunication);
                        MainViewModel.SubContent = tcpcommunication;
                        MainViewModel.Pagetitle = model.Name;
                        break;
                    case NodeType.Serials:
                        var serialscommunication = new UCCommunication();
                        serialscommunication.viewModel.Type = SlaveType.Serials;
                        PageVMHelper.LoadCommunicationContent(serialscommunication);
                        MainViewModel.SubContent = serialscommunication;
                        MainViewModel.Pagetitle = model.Name;
                        break;
                    case NodeType.TCPNode:
                        var tcpcontent = new UCModbusTCP();
                        PageVMHelper.LoadTCPContent(tcpcontent);
                        MainViewModel.SubContent = tcpcontent;
                        MainViewModel.Pagetitle = model.Name;
                        break;
                    case NodeType.SerialNode:
                        var serialcontent = new UCModbusSerial();
                        PageVMHelper.LoadSerialContent(serialcontent);
                        MainViewModel.SubContent = serialcontent;
                        MainViewModel.Pagetitle = model.Name;
                        break;
                    case NodeType.SerialDevice:
                        var serialdevice = new UCDevice();
                        (serialdevice.DataContext as DeviceViewModel).GatewayName = model.ParentNode.Name;
                        MainViewModel.SubContent = serialdevice;
                        PageVMHelper.LoadDeviceContent(serialdevice);
                        MainViewModel.Pagetitle = model.Name;
                        break;
                    case NodeType.TCPDevice:
                        var tcpdevice = new UCDevice();
                        (tcpdevice.DataContext as DeviceViewModel).GatewayName = model.ParentNode.Name;
                        MainViewModel.SubContent = tcpdevice;
                        PageVMHelper.LoadDeviceContent(tcpdevice);
                        MainViewModel.Pagetitle = model.Name;
                        break;
                    default:
                        break;
                }
               
            }
        }






    }




}




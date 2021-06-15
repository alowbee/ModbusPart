using CNC_SNC_CSharp;
using ModbusInfoPart.Data;
using ModbusPart.Data;
using ModbusPart.Sub;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using WPFToolkit.Controls;
using WPFToolkit.Data.Enum;

namespace ModbusPart.ViewModel
{
    public class TCPViewModel : BindableBase
    {
        private bool isEnable;
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                RaisePropertyChanged(nameof(IsEnable));

                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.TCPNode)
                        return;
                    var index = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[index].bEnable = IsEnable;
                    UCModbus.FileSaveTrg = true;
                }

            }
        }


        private string gatewayName;
        public string GatewayName
        {
            get { return gatewayName; }
            set
            {
                gatewayName = value;
                RaisePropertyChanged(nameof(GatewayName));

                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.TCPNode)
                        return;
                    var index = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[index].TCPName = GatewayName;
                    UCModbus.MainViewModel.Pagetitle = GatewayName;
                    UCModbus.MainViewModel.CurrentNode.Name = GatewayName;
                    UCModbus.FileSaveTrg = true;

                }


            }
        }

        private string iP;
        public string IP
        {
            get { return iP; }
            set
            {
                iP = value;
                RaisePropertyChanged(nameof(IP));

                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.TCPNode)
                        return;
                    var index = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[index].ip = IP;
                    UCModbus.FileSaveTrg = true;
                }


            }
        }


        private int port;
        public int Port
        {
            get { return port; }
            set
            {
                port = value;
                RaisePropertyChanged(nameof(Port));

                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.TCPNode)
                        return;
                    var index = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[index].port = Port;
                    UCModbus.FileSaveTrg = true;
                }


            }
        }

        private int retry;
        public int Retry
        {
            get { return retry; }
            set
            {
                retry = value;
                RaisePropertyChanged(nameof(Retry));

                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.TCPNode)
                        return;
                    var index = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[index].Retry = Retry;
                    UCModbus.FileSaveTrg = true;
                }

            }
        }



        private int timeOut;
        public int TimeOut
        {
            get { return timeOut; }
            set
            {
                timeOut = value;
                RaisePropertyChanged(nameof(TimeOut));


                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.TCPNode)
                        return;
                    var index = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[index].TimeOut = TimeOut;
                    UCModbus.FileSaveTrg = true;
                }

            }
        }


        private int updatePeriod;
        public int UpdatePeriod
        {
            get { return updatePeriod; }
            set
            {
                updatePeriod = value;
                RaisePropertyChanged(nameof(UpdatePeriod));


                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.TCPNode)
                        return;
                    var index = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[index].Timer = UpdatePeriod;
                    UCModbus.FileSaveTrg = true;
                }
            }
        }

        private int deviceNum;
        public int DeviceNum
        {
            get { return deviceNum; }
            set
            {
                deviceNum = value;
                RaisePropertyChanged(nameof(DeviceNum));
            }
        }


        public DelegateCommand AddDeviceCommand { get; set; }
        public DelegateCommand DeletePortCommand { get; set; }



        /// <summary>
        /// 添加设备子节点
        /// </summary>
        private void AddDevice()
        {

            if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.TCPNode)
                return;
            //範圍限制
            int child_cnt = UCModbus.MainViewModel.CurrentNode.Children.Count;
            var index = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
            if (child_cnt >= ModbusInfo.nDeviceNUM - 1)
            {
                var Str = "";
                Str = "[" + ModbusInfo.TCP[index].TCPName + "] : \r\n";
                // Device amount out of range : \r\n\tmaximum 
                // MessageBox.Show(Str + Localization.Modbus_Form_Mesg[Localization.Instance.language_index, 28] + (Modbus.nDeviceNUM - 1).ToString(), Localization.System_Mesg[Localization.Instance.language_index, 1], MessageBoxButtons.OK, MessageBoxIcon.Error);
                ToolkitMessageBox.Show("Device amount out of range :" + (ModbusInfo.nDeviceNUM - 1).ToString(), "out of range", MessageBoxButton.OKCancel, InfoType.Error);
                return;
            }


            //界面添加節點
            var devicenode = new TreeViewNode();
            devicenode.NodeType = NodeType.TCPDevice;
            devicenode.ParentNode = UCModbus.MainViewModel.CurrentNode;
            devicenode.ParentNode.IsExpanded = true;

            var New_Name = "";
            for (int idx = 1; idx < ModbusInfo.nDeviceNUM; idx++)
            {
                bool Name_check = false;
                foreach (var ModbusTCP in ModbusInfo.TCP[index].deviceName)
                {
                    if (ModbusTCP == "Device " + idx.ToString())
                    {
                        Name_check = true;
                        break;
                    }
                }
                if (!Name_check)
                {
                    New_Name = "Device " + idx.ToString();
                    break;
                }
            }
            devicenode.Name = New_Name;
            UCModbus.MainViewModel.CurrentNode.Children.Add(devicenode);

            if (UCModbus.MainViewModel.SubContent is UCModbusTCP tcpcontent)
            {
                tcpcontent.viewModel.DeviceNum++;
            }

            ///*Serial device num*/
            ModbusInfo.TCP[index].NUM = child_cnt + 1;
            // textBox_TCPTag_Amount.Text = Modbus.TCP[GParameter.Modbus_Port_node].NUM.ToString() + " / " + Modbus.nDeviceNUM.ToString();

            /*device type*/
            ModbusInfo.TCP[index].strType[child_cnt] = "Device";
            ModbusInfo.TCP[index].Station[child_cnt] = 1;
            ModbusInfo.TCP[index].deviceName[child_cnt] = New_Name;
            ModbusInfo.TCP[index].EnableTCPDevice[child_cnt] = true;
            UCModbus.FileSaveTrg = true;
 

        }

        /// <summary>
        /// 删除TCP节点
        /// </summary>
        private void DeletePort()
        {
            if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.TCPNode)
                return;
            var index = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
            for (int i = index; i < ModbusInfo.nTCPNUM; i++)
            {
                ModbusInfo.TCP[i] = ModbusInfo.TCP[i + 1];

            }
            ModbusInfo.TCP_Amount--;
            UCModbus.MainViewModel.TCPMainNode.Children.Remove(UCModbus.MainViewModel.CurrentNode);
            UCModbus.FileSaveTrg = true;
    


        }







        public TCPViewModel()
        {
            AddDeviceCommand = new DelegateCommand(AddDevice);
            DeletePortCommand = new DelegateCommand(DeletePort);
        }



    }
}

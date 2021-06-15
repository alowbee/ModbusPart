using CNC_SNC_CSharp;
using ModbusInfoPart.Data;
using ModbusPart.Data;
using ModbusPart.Sub;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace ModbusPart.ViewModel
{
    public class SerialViewModel : BindableBase
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
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].bEnable = IsEnable;
                    UCModbus.FileSaveTrg = true;
                }

              
            }
        }

        private int protocol;
        public int Protocol
        {
            get { return protocol; }
            set
            {
                protocol = value;
                RaisePropertyChanged(nameof(Protocol));

                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].RTUASC = Protocol;
                }
            }
        }

        private int parity;
        public int Parity
        {
            get { return parity; }
            set
            {
                parity = value;
                RaisePropertyChanged(nameof(Parity));
                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].Parity = Parity;
                    UCModbus.FileSaveTrg = true;
                }

            }
        }

        private int com;
        public int Com
        {
            get { return com; }
            set
            {
                com = value;
                RaisePropertyChanged(nameof(Com));
                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    if (Com < 0)
                        ModbusInfo.Serial[index].COM = -1;
                    else
                        ModbusInfo.Serial[index].COM = Convert.ToInt32(ComList[Com].Remove(0, 3));
                    UCModbus.FileSaveTrg = true;


                }

            }
        }

        private int baudrate;
        public int Baudrate
        {
            get { return baudrate; }
            set
            {
                baudrate = value;
                RaisePropertyChanged(nameof(Baudrate));
                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].Baudrate = Baudrate;
                    UCModbus.FileSaveTrg = true;
                }
            }
        }

        private int dataBit;
        public int DataBit
        {
            get { return dataBit; }
            set
            {
                dataBit = value;
                RaisePropertyChanged(nameof(DataBit));
                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].DataBit = dataBit;
                    UCModbus.FileSaveTrg = true;
                }
            }
        }

        private int stopBit;
        public int StopBit
        {
            get { return stopBit; }
            set
            {
                stopBit = value;
                RaisePropertyChanged(nameof(StopBit));
                if (UCModbus.MainViewModel.CurrentNode != null)
                {
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].StopBit = StopBit;
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
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].portName = GatewayName;
                    UCModbus.MainViewModel.Pagetitle = GatewayName;
                    UCModbus.MainViewModel.CurrentNode.Name = GatewayName;
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
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].Retry = Retry;
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
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].TimeOut = TimeOut;
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
                    if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                        return;
                    var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[index].Timer = UpdatePeriod;
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

        private ObservableCollection<string> comList;
        public ObservableCollection<string> ComList
        {
            get { return comList; }
            private set
            {
                comList = value;
                RaisePropertyChanged(nameof(ComList));
            }
        }

        public DelegateCommand AddDeviceCommand { get; set; }
        public DelegateCommand DeletePortCommand { get; set; }


        /// <summary>
        /// 添加设备子节点
        /// </summary>
        private void AddDevice()
        {

            if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                return;

            //範圍限制
            int child_cnt = UCModbus.MainViewModel.CurrentNode.Children.Count;
            var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
            if (child_cnt >= ModbusInfo.nSerialNUM - 1)
            {
                var Str = "";
                Str = "[" + ModbusInfo.Serial[index].portName + "] : \r\n";
                // Device amount out of range : \r\n\tmaximum 
                // MessageBox.Show(Str + Localization.Modbus_Form_Mesg[Localization.Instance.language_index, 28] + (Modbus.nDeviceNUM - 1).ToString(), Localization.System_Mesg[Localization.Instance.language_index, 1], MessageBoxButtons.OK, MessageBoxIcon.Error);
                WPFToolkit.Controls.ToolkitMessageBox.Show("Device amount out of range :" + (ModbusInfo.nSerialNUM - 1).ToString(), "out of range", System.Windows.MessageBoxButton.OKCancel, WPFToolkit.Data.Enum.InfoType.Error);
                return;
            }


            //界面添加節點

            var devicenode = new TreeViewNode();
            devicenode.NodeType = NodeType.SerialDevice;
            devicenode.ParentNode = UCModbus.MainViewModel.CurrentNode;
            devicenode.ParentNode.IsExpanded = true;

            var New_Name = "";
            for (int idx = 1; idx < ModbusInfo.nDeviceNUM; idx++)
            {
                bool Name_check = false;
                foreach (var ModbusSerial in ModbusInfo.Serial[index].deviceName)
                {
                    if (ModbusSerial == "Device " + idx.ToString())
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

            if (UCModbus.MainViewModel.SubContent is UCModbusSerial serialcontent)
            {
                serialcontent.viewModel.DeviceNum++;
            }

            ///*Serial device num*/
            ModbusInfo.Serial[index].NUM = child_cnt + 1;
            // textBox_TCPTag_Amount.Text = Modbus.TCP[GParameter.Modbus_Port_node].NUM.ToString() + " / " + Modbus.nDeviceNUM.ToString();

            /*device type*/
            ModbusInfo.Serial[index].strType[child_cnt] = "Device";
            ModbusInfo.Serial[index].Station[child_cnt] = 1;
            ModbusInfo.Serial[index].deviceName[child_cnt] = New_Name;
            ModbusInfo.Serial[index].EnableSerialDevice[child_cnt] = true;
            UCModbus.FileSaveTrg = true;
   

        }

        /// <summary>
        /// 删除串口节点
        /// </summary>
        private void DeletePort()
        {
            if (UCModbus.MainViewModel.CurrentNode.NodeType != NodeType.SerialNode)
                return;

            var index = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
            for (int i = index; i < ModbusInfo.nSerialNUM; i++)
            {
                ModbusInfo.Serial[i] = ModbusInfo.Serial[i + 1];

            }
            ModbusInfo.COM_Amount--;
            UCModbus.MainViewModel.SerialMainNode.Children.Remove(UCModbus.MainViewModel.CurrentNode);
            UCModbus.FileSaveTrg = true;
    


        }


        public SerialViewModel()
        {
            foreach (var item in System.IO.Ports.SerialPort.GetPortNames())
            {
                ComList = new ObservableCollection<string>();
                ComList.Add(item);
            }

            this.AddDeviceCommand = new DelegateCommand(AddDevice);
            this.DeletePortCommand = new DelegateCommand(DeletePort);
        }

    }
}

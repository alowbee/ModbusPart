using CNC_SNC_CSharp;
using ModbusInfoPart.Data;
using ModbusPart.Data;
using ModbusPart.Sub;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace ModbusPart.ViewModel
{

    public enum SlaveType
    {
        TCP = 0,
        Serials
    }

    public class CommunicationViewModel : BindableBase
    {
        private int slaveStation;
        public int SlaveStation
        {
            get { return slaveStation; }
            set
            {
                slaveStation = value;
                RaisePropertyChanged(nameof(SlaveStation));
                ModbusInfo.Server_Station = SlaveStation;
                UCModbus.FileSaveTrg = true;
            }
        }

        private SlaveType type;
        public SlaveType Type
        {
            get { return type; }
            set
            {
                type = value;
                RaisePropertyChanged(nameof(Type));

            }
        }


        private int tcpPort;
        public int TcpPort
        {
            get { return tcpPort; }
            set
            {
                tcpPort = value;
                RaisePropertyChanged(nameof(TcpPort));
                ModbusInfo.Server_TCP_Port = TcpPort;
                UCModbus.FileSaveTrg = true;
            }
        }


        private int tcpPortNum;
        public int TcpPortNum
        {
            get { return tcpPortNum; }
            set
            {
                tcpPortNum = value;
                RaisePropertyChanged(nameof(TcpPortNum));
                ModbusInfo.TCP_Amount = TcpPortNum;
                UCModbus.FileSaveTrg = true;
            }
        }

        private int comPortNum;
        public int ComPortNum
        {
            get { return comPortNum; }
            set
            {
                comPortNum = value;
                RaisePropertyChanged(nameof(ComPortNum));
                ModbusInfo.COM_Amount = ComPortNum;
                UCModbus.FileSaveTrg = true;
            }
        }


        public DelegateCommand AddCom { get; set; }
        public DelegateCommand AddTcp { get; set; }


        /// <summary>
        /// 添加TCP节点
        /// </summary>
        private void AddTCPNode()
        {
            //超出界限
            if (ModbusInfo.TCP_Amount >= ModbusInfo.nTCPNUM)
            {
                // TCP amount out of range : \r\n\tmaximum 
                WPFToolkit.Controls.ToolkitMessageBox.Show("TCP amount out of range:" + ModbusInfo.nTCPNUM.ToString(), "Out of range", System.Windows.MessageBoxButton.OKCancel,WPFToolkit.Data.Enum.InfoType.Error);
                return;
            }
            //界面新增节点
            var node = new TreeViewNode();
            node.NodeType = NodeType.TCPNode;
            node.ParentNode = UCModbus.MainViewModel.TCPMainNode;


            //界面命名
            var New_Name = "";
            var index = 0;
            for (int idx = 1; idx <= ModbusInfo.nTCPNUM; idx++)
            {
                bool Name_check = false;
                foreach (ModbusInfo.ModbusTCP ModbusTCP in ModbusInfo.TCP)
                {
                    if (ModbusTCP.TCPName == "Modbus TCP " + Convert.ToString(idx))
                    {
                        Name_check = true;
                        break;
                    }
                }
                if (!Name_check)
                {
                    New_Name = "Modbus TCP " + Convert.ToString(idx);
                    index = idx;
                    break;
                }
            }
            node.Name = New_Name;
            UCModbus.MainViewModel.TCPMainNode.Children.Add(node);

            if (UCModbus.MainViewModel.SubContent is UCCommunication communicationcontent)
            {
                communicationcontent.viewModel.TcpPortNum++;

            }


            //数据储存
            ModbusInfo.TCP[ModbusInfo.TCP_Amount - 1].bEnable = true;
            ModbusInfo.TCP[ModbusInfo.TCP_Amount - 1].TCPName = New_Name;
            UCModbus.FileSaveTrg = true;
     
        }

        /// <summary>
        /// 添加Com节点
        /// 
        /// </summary>
        private void AddComNode()
        {
            //超出界限
            if (ModbusInfo.COM_Amount >= ModbusInfo.nSerialNUM)
            {
                // Serial COM amount out of range : \r\n\tmaximum 
                WPFToolkit.Controls.ToolkitMessageBox.Show(" Serial COM amount out of range :" + ModbusInfo.nSerialNUM.ToString(), "Out of range", System.Windows.MessageBoxButton.OKCancel,WPFToolkit.Data.Enum.InfoType.Error);
                return;
            }
            //界面新增节点
            var node = new TreeViewNode();
            node.NodeType = NodeType.SerialNode;
            node.ParentNode = UCModbus.MainViewModel.SerialMainNode;

            //界面命名
            var New_Name = "";
            for (int idx = 1; idx <= ModbusInfo.nSerialNUM; idx++)
            {
                bool Name_check = false;
                foreach (ModbusInfo.ModbusSerial ModbusSerial in ModbusInfo.Serial)
                {
                    if (ModbusSerial.portName == "Modbus Serial " + Convert.ToString(idx))
                    {
                        Name_check = true;
                        break;
                    }
                }
                if (!Name_check)
                {
                    New_Name = "Modbus Serial " + Convert.ToString(idx);
                    break;
                }
            }
            node.Name = New_Name;
            UCModbus.MainViewModel.SerialMainNode.Children.Add(node);

            if (UCModbus.MainViewModel.SubContent is UCCommunication communicationcontent)
            {
                communicationcontent.viewModel.ComPortNum++;
            }

            //数据储存
            ModbusInfo.Serial[ModbusInfo.COM_Amount - 1].bEnable = true;
            ModbusInfo.Serial[ModbusInfo.COM_Amount - 1].portName = New_Name;
            UCModbus.FileSaveTrg = true;

        }


        public CommunicationViewModel()
        {
            this.AddCom = new DelegateCommand(AddComNode);
            this.AddTcp = new DelegateCommand(AddTCPNode);
        }

    }
}

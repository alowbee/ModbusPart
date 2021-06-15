using CNC_SNC_CSharp;
using EndUser;
using ModbusInfoPart.Data;
using ModbusPart.Data;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Animation;

namespace ModbusPart.ViewModel
{
    public class ModusViewModel : BindableBase
    {
        private object subContent;
        public object SubContent
        {
            get { return subContent; }
            set
            {
                subContent = value;
                RaisePropertyChanged(nameof(SubContent));
            }
        }



        private string pagetile;
        public string Pagetitle
        {
            get { return pagetile; }
            set
            {
                pagetile = value;
                RaisePropertyChanged(nameof(Pagetitle));
            }
        }


        private ObservableCollection<TreeViewNode> nodeTreeItems;
        public ObservableCollection<TreeViewNode> NodeTreeItems
        {
            get { return nodeTreeItems; }
            set
            {
                nodeTreeItems = value;
                RaisePropertyChanged(nameof(NodeTreeItems));
            }
        }

        private TreeViewNode currentNode;
        public TreeViewNode CurrentNode

        {
            get { return currentNode; }
            set
            {
                currentNode = value;
                RaisePropertyChanged(nameof(CurrentNode));
            }
        }


        public TreeViewNode MainNode = new TreeViewNode();
        public TreeViewNode TCPMainNode = new TreeViewNode();
        public TreeViewNode SerialMainNode = new TreeViewNode();

        /// <summary>
        ///將Modbus存儲數據
        /// </summary>
        public void LoadNodesFromData()
        {
            //Load Tree TCP
            for (int nTCP = 0; nTCP < (int)Class_InifileRW.ini_ReadInteger("Config", "TCPNUM", 0); nTCP++)
            {
                var TcpNoTreeNode = new TreeViewNode();
                TcpNoTreeNode.IsExpanded = false;
                TcpNoTreeNode.Name = ModbusInfo.TCP[nTCP].TCPName;
                TcpNoTreeNode.ToolTip = "Modbus TCP" + " port:" + (nTCP + 1).ToString();
                TcpNoTreeNode.NodeType = NodeType.TCPNode;
                TcpNoTreeNode.ParentNode = TCPMainNode;
                TCPMainNode.Children.Add(TcpNoTreeNode);

                //Load Tree TCP device
                var Devicenum = (int)Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString(), "Num", 0);//20181004修正讀取ini異常NUM>>Num
                for (int nDevice_node = 0; nDevice_node < Devicenum; nDevice_node++)
                {
                    var TcpDeviceNoTreeNode = new TreeViewNode();
                    TcpDeviceNoTreeNode.Name = ModbusInfo.TCP[nTCP].deviceName[nDevice_node];
                    TcpDeviceNoTreeNode.ToolTip = "Modbus TCP" + " port:" + (nTCP + 1).ToString() + " device:" + (nDevice_node + 1).ToString();
                    TcpDeviceNoTreeNode.NodeType = NodeType.TCPDevice;
                    TcpDeviceNoTreeNode.ParentNode = TcpNoTreeNode;
                    TcpNoTreeNode.Children.Add(TcpDeviceNoTreeNode);
                }

            }
            //Load Tree Serial Port
            for (int nSerial = 0; nSerial < (int)Class_InifileRW.ini_ReadInteger("Config", "COMNUM", 0); nSerial++)
            {
                var portNoTreeNode = new TreeViewNode();
                portNoTreeNode.IsExpanded = false;
                portNoTreeNode.Name = ModbusInfo.Serial[nSerial].portName;
                portNoTreeNode.ToolTip = "Modbus Serial" + " port:" + (nSerial + 1).ToString();
                portNoTreeNode.NodeType = NodeType.SerialNode;
                portNoTreeNode.ParentNode = SerialMainNode;
                SerialMainNode.Children.Add(portNoTreeNode);

                //Load Tree Serial device
                var Devicenum = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "NUM", 0);
                for (int nDevice_node = 0; nDevice_node < Devicenum; nDevice_node++)
                {

                    var nodeNoTreeNode = new TreeViewNode();
                    nodeNoTreeNode.Name = ModbusInfo.Serial[nSerial].deviceName[nDevice_node];
                    nodeNoTreeNode.ToolTip = "Modbus Serial" + " port:" + (nSerial + 1).ToString() + " device:" + (nDevice_node + 1).ToString();
                    nodeNoTreeNode.NodeType = NodeType.SerialDevice;
                    nodeNoTreeNode.ParentNode = portNoTreeNode;
                    portNoTreeNode.Children.Add(nodeNoTreeNode);
                }
            }
        }

        /// <summary>
        /// 组织成树
        /// </summary>
        public void LoadDefaultNodeTree()
        {

            TCPMainNode.Name = "Modbus TCP";
            TCPMainNode.IsSelected = true;//默认选择TCPMainNode
            CurrentNode = TCPMainNode;
            TCPMainNode.ParentNode = MainNode;
            TCPMainNode.NodeType = NodeType.TCP;
            TCPMainNode.IsExpanded = true;

            SerialMainNode.Name = "Modbus Serial";
            SerialMainNode.ParentNode = MainNode;
            SerialMainNode.NodeType = NodeType.Serials;
            SerialMainNode.IsExpanded = true;

            MainNode.Name = "Modbus Communication";
            MainNode.Children.Add(TCPMainNode);
            MainNode.Children.Add(SerialMainNode);
            MainNode.NodeType = NodeType.IMP;
            MainNode.IsExpanded = true;
            NodeTreeItems.Add(MainNode);
        }

        public DelegateCommand<object> CloseTreeViewCommand { get; private set; }
        public DelegateCommand<object> OpenTreeViewCommand { get; private set; }

        public void CloseTreeViewExcute(object param)
        {
            if (param is FrameworkElement element)
            {
                ThicknessAnimation closetreeview = new ThicknessAnimation();
                closetreeview.From = new Thickness(0, 0, 0, 0);
                closetreeview.To = new Thickness(-element.ActualWidth, 0, 0, 0);
                closetreeview.Duration = TimeSpan.FromSeconds(0.4);
                element.BeginAnimation(FrameworkElement.MarginProperty, closetreeview);
            }

        }

        public void OpenTreeViewExcute(object param)
        {
            if (param is FrameworkElement element)
            {
                ThicknessAnimation opentreeview = new ThicknessAnimation();
                opentreeview.From = new Thickness(-element.ActualWidth, 0, 0, 0);
                opentreeview.To = new Thickness(0, 0, 0, 0);
                opentreeview.Duration = TimeSpan.FromSeconds(0.4);
                element.BeginAnimation(FrameworkElement.MarginProperty, opentreeview);
            }
        }

        public ModusViewModel()
        {
            this.CloseTreeViewCommand = new DelegateCommand<object>(this.CloseTreeViewExcute);
            this.OpenTreeViewCommand = new DelegateCommand<object>(this.OpenTreeViewExcute);
            NodeTreeItems = new ObservableCollection<TreeViewNode>();
            DataHelper.LoadIniData();
            LoadNodesFromData();
           LoadDefaultNodeTree();

        }

    }
}

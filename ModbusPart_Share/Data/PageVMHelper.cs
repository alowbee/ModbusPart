using CNC_SNC_CSharp;
using EndUser;
using ModbusPart.Sub;
using ModbusPart.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;

namespace ModbusPart.Data
{
    /// <summary>
    /// 界面控件读取ModbusInfo
    /// </summary>
    public static class PageVMHelper
    {
        /// <summary>
        /// Communication页面 VM数据对接
        /// </summary>
        /// <param name="vm"></param>
        public static void LoadCommunicationContent(UCCommunication Content)
        {

            Content.viewModel.SlaveStation = ModbusInfo.Server_Station;
            if (Content.viewModel.Type == SlaveType.TCP)
            {
                Content.viewModel.TcpPort = ModbusInfo.Server_TCP_Port;
                Content.viewModel.TcpPortNum = ModbusInfo.TCP_Amount;
            }
            else if (Content.viewModel.Type == SlaveType.Serials)
            {
                Content.viewModel.ComPortNum = ModbusInfo.COM_Amount;
            }

        }



        /// <summary>
        ///TCP页面 VM数据对接
        /// </summary>
        public static void LoadTCPContent(UCModbusTCP Content)
        {
            var current = UCModbus.MainViewModel.CurrentNode;
            var parent = UCModbus.MainViewModel.TCPMainNode;
            var nTCP = parent.Children.IndexOf(current);
            var cuurenttcp = Content.viewModel;

            cuurenttcp.IsEnable = ModbusInfo.TCP[nTCP].bEnable;
            cuurenttcp.GatewayName = ModbusInfo.TCP[nTCP].TCPName;
            cuurenttcp.IP = ModbusInfo.TCP[nTCP].ip;
            cuurenttcp.Port = ModbusInfo.TCP[nTCP].port;
            cuurenttcp.Retry = ModbusInfo.TCP[nTCP].Retry;
            cuurenttcp.TimeOut = ModbusInfo.TCP[nTCP].TimeOut;
            cuurenttcp.UpdatePeriod = ModbusInfo.TCP[nTCP].Timer;
            cuurenttcp.DeviceNum = ModbusInfo.TCP[nTCP].NUM;
        }

        /// <summary>
        /// Serial页面VM数据对接
        /// </summary>
        public static void LoadSerialContent(UCModbusSerial Content)
        {
            var current = UCModbus.MainViewModel.CurrentNode;
            var parent = UCModbus.MainViewModel.SerialMainNode;
            var nSerial = parent.Children.IndexOf(current);
            var currentserial = Content.viewModel;

            currentserial.IsEnable = ModbusInfo.Serial[nSerial].bEnable;
            currentserial.GatewayName = ModbusInfo.Serial[nSerial].portName;
            var comlist = System.IO.Ports.SerialPort.GetPortNames();
            var comindex = -1;
            for (int i = 0; i < comlist.Length; i++)
            {
                if (comlist[i] == "COM" + ModbusInfo.Serial[nSerial].COM)
                    comindex = i;
            }
            currentserial.Com = comindex;
            if (Class_InifileRW.GetIniDataToCString("SERIAL" + (nSerial + 1).ToString(), "RTUASC", "") != "")
                currentserial.Protocol = ModbusInfo.Serial[nSerial].RTUASC;
            currentserial.Baudrate = ModbusInfo.Serial[nSerial].Baudrate;
            currentserial.Parity = ModbusInfo.Serial[nSerial].Parity;
            currentserial.DataBit = ModbusInfo.Serial[nSerial].DataBit;
            currentserial.StopBit = ModbusInfo.Serial[nSerial].StopBit;
            currentserial.Retry = ModbusInfo.Serial[nSerial].Retry;
            currentserial.TimeOut = ModbusInfo.Serial[nSerial].TimeOut;
            currentserial.UpdatePeriod = ModbusInfo.Serial[nSerial].Timer;
            if (currentserial.UpdatePeriod <= 10)
                currentserial.UpdatePeriod = 10;
            currentserial.DeviceNum = ModbusInfo.Serial[nSerial].NUM;

        }


        internal static void SetMapitemIndex(ObservableCollection<MapItem> map)
        {
            for (int i = 0; i < map.Count; i++)
            {
                map[i].Index = i + 1;
            }

        }

        //设置按钮名称
        private static string GetButtonContent(string initext)
        {
            if (initext == "0")
                return Properties.Lang.Lang.Disable;
            else if (initext == "1")
                return Properties.Lang.Lang.Enable;
            else
                return initext.ToUpper();

        }

        /// <summary>
        /// Device页面数据绑定
        /// </summary>
        public static void LoadDeviceContent(UCDevice Content)
        {
            var current = UCModbus.MainViewModel.CurrentNode;
            var currentdevice = Content.viewModel;

            if (current.NodeType == NodeType.SerialDevice)
            {
                var main = UCModbus.MainViewModel.SerialMainNode;
                var parent = UCModbus.MainViewModel.SerialMainNode.Children.Where(p => p.Children.Contains(current)).First();
                var nSerial = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(parent);
                var nDevice_node = parent.Children.IndexOf(current);
                currentdevice.IsEnable = ModbusInfo.Serial[nSerial].EnableSerialDevice[nDevice_node];
                currentdevice.Name = ModbusInfo.Serial[nSerial].deviceName[nDevice_node];
                currentdevice.Station = ModbusInfo.Serial[nSerial].Station[nDevice_node];
                currentdevice.Error_reg = ModbusInfo.Serial[nSerial].Error_reg[nDevice_node];
                currentdevice.Device_Read_Limitlen = ModbusInfo.Serial[nSerial].Device_Read_Limitlen[nDevice_node];

                //读取已有数据
                string[] readmapdata_ini = new string[7];
                string[] writemapdata_ini = new string[7];
                for (int i = 0; i < ModbusInfo.nTagNUM; i++)
                {
                    var readinfo = ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i];
                    var writeinfo = ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i];

                    readmapdata_ini = readinfo.Split(',');
                    writemapdata_ini = writeinfo.Split(',');

                    // 沒有資料
                    if (readmapdata_ini.Length >= 5)
                    {
                        var tempmapitem = new MapItem();
                        tempmapitem.Function = Convert.ToInt32(readmapdata_ini[0], 16);
                        tempmapitem.TagAddress = readmapdata_ini[1];
                        tempmapitem.Register = Convert.ToInt32(readmapdata_ini[2]);
                        tempmapitem.Length = Convert.ToInt32(readmapdata_ini[4]);
                        tempmapitem.ButtonContent = GetButtonContent(readmapdata_ini[3]);
                        Content.viewModel.ReadMap.Add(tempmapitem);
                    }

                    if (writemapdata_ini.Length >= 5)
                    {
                        var tempmapitem = new MapItem();
                        var tempfunc = Convert.ToInt32(writemapdata_ini[0], 16);
                        if (tempfunc > 14)
                            tempmapitem.Function = tempfunc - 10;
                        else
                            tempmapitem.Function = tempfunc;
                        tempmapitem.TagAddress = writemapdata_ini[1];
                        tempmapitem.Register = Convert.ToInt32(writemapdata_ini[2]);
                        tempmapitem.Length = Convert.ToInt32(writemapdata_ini[4]);
                        tempmapitem.ButtonContent = GetButtonContent(writemapdata_ini[3]);
                        Content.viewModel.WriteMap.Add(tempmapitem);
                    }



                }
                //增加一行空白數據
                currentdevice.ReadMap.Add(new MapItem());
                currentdevice.WriteMap.Add(new MapItem());
            }
            else if (current.NodeType == NodeType.TCPDevice)
            {
                var main = UCModbus.MainViewModel.TCPMainNode;
                var parentlist = from item in UCModbus.MainViewModel.TCPMainNode.Children where item.Children.Contains(current) select item;
                var parent = parentlist.First();
                var nTCP = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(parent);
                var nDevice_node = parent.Children.IndexOf(current);

                currentdevice.IsEnable = ModbusInfo.TCP[nTCP].EnableTCPDevice[nDevice_node];
                currentdevice.Name = ModbusInfo.TCP[nTCP].deviceName[nDevice_node];
                currentdevice.Station = ModbusInfo.TCP[nTCP].Station[nDevice_node];
                currentdevice.Error_reg = ModbusInfo.TCP[nTCP].Error_reg[nDevice_node];
                currentdevice.Device_Read_Limitlen = ModbusInfo.TCP[nTCP].Device_Read_Limitlen[nDevice_node];
                if (Content.viewModel.Device_Read_Limitlen < 1)
                    Content.viewModel.Device_Read_Limitlen = 100;

                //读取已有数据
                string[] readmapdata_ini = new string[7];
                string[] writemapdata_ini = new string[7];
                for (int i = 0; i < ModbusInfo.nTagNUM; i++)
                {
                    var readinfo = ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i];
                    var writeinfo = ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i];

                    readmapdata_ini = readinfo.Split(',');
                    writemapdata_ini = writeinfo.Split(',');

                    // 沒有資料
                    if (readmapdata_ini.Length >= 5)
                    {
                        var tempmapitem = new MapItem();
                        tempmapitem.Function = Convert.ToInt32(readmapdata_ini[0]);
                        tempmapitem.TagAddress = readmapdata_ini[1];
                        tempmapitem.Register = Convert.ToInt32(readmapdata_ini[2]);
                        tempmapitem.Length = Convert.ToInt32(readmapdata_ini[4]);
                        tempmapitem.ButtonContent = GetButtonContent(readmapdata_ini[3]);
                        Content.viewModel.ReadMap.Add(tempmapitem);
                    }

                    if (writemapdata_ini.Length >= 5)
                    {
                        var tempmapitem = new MapItem();
                        var tempfunc = Convert.ToInt32(writemapdata_ini[0], 16);
                        if (tempfunc > 14)
                            tempmapitem.Function = tempfunc - 10;
                        else
                            tempmapitem.Function = tempfunc;
                        tempmapitem.TagAddress = writemapdata_ini[1];
                        tempmapitem.Register = Convert.ToInt32(writemapdata_ini[2]);
                        tempmapitem.Length = Convert.ToInt32(writemapdata_ini[4]);
                        tempmapitem.ButtonContent = GetButtonContent(writemapdata_ini[3]);
                        Content.viewModel.WriteMap.Add(tempmapitem);
                    }



                }
                //增加一行空白數據
                currentdevice.ReadMap.Add(new MapItem());
                currentdevice.WriteMap.Add(new MapItem());

            }

            SetMapitemIndex(currentdevice.ReadMap);
            SetMapitemIndex(currentdevice.WriteMap);
        }


    }
}

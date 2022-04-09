using CNC_SNC_CSharp;
using EndUser;
using ModbusPart;
using ModbusPart.Data;
using ModbusPart.Sub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WPFToolkit.Data.Enum;

namespace ModbusInfoPart.Data
{
    public class TallItem
    {
        public string STR { set; get; }
        public int REG { set; get; }
    }

    //ini文件与ModbusInfo读取

    public static class DataHelper
    {

        public static void SetReadMap()
        {
            if (UCModbus.MainViewModel.CurrentNode != null)
            {
                var currentnode = UCModbus.MainViewModel.CurrentNode;
                if (UCModbus.MainViewModel.SubContent is UCDevice device)
                {
                    var read_map = device.viewModel.ReadMap;
                    if (currentnode.NodeType == NodeType.TCPDevice)
                    {
                        var nTCP = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(currentnode.ParentNode);
                        var nDevice_node = currentnode.ParentNode.Children.IndexOf(currentnode);
                        for (int i = 0; i < read_map.Count; i++)
                        {
                            if (read_map[i].IsComplete == true)
                            {
                                ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] = "";
                                ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] += (read_map[i].Function).ToString().PadLeft(2, '0') + ",";
                                ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] += read_map[i].TagAddress + ",";
                                ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] += read_map[i].Register + ",";
                                var temp = "";
                                if (read_map[i].ButtonContent == ModbusPart.Properties.Lang.Lang.Enable)
                                    temp = "1";
                                else if (read_map[i].ButtonContent == ModbusPart.Properties.Lang.Lang.Disable)
                                    temp = "0";
                                else temp = read_map[i].ButtonContent.ToLower();
                                ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] += temp + ",";
                                ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] += read_map[i].Length + ",";
                                ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] += ",";
                                ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] += i.ToString();

                            }
                            else ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] = "";


                        }
                        for (int i = read_map.Count - 1; i < ModbusInfo.nTagNUM; i++)
                        {
                            ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, i] = "";
                        }

                    }
                    if (currentnode.NodeType == NodeType.SerialDevice)
                    {
                        var nSerial = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(currentnode.ParentNode);
                        var nDevice_node = currentnode.ParentNode.Children.IndexOf(currentnode);
                        for (int i = 0; i < read_map.Count; i++)
                        {
                            if (read_map[i].IsComplete == true)
                            {
                                ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] = "";
                                ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] += (read_map[i].Function).ToString().PadLeft(2, '0') + ",";
                                ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] += read_map[i].TagAddress + ",";
                                ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] += read_map[i].Register + ",";
                                var temp = "";
                                if (read_map[i].ButtonContent == ModbusPart.Properties.Lang.Lang.Enable)
                                    temp = "1";
                                else if (read_map[i].ButtonContent == ModbusPart.Properties.Lang.Lang.Disable)
                                    temp = "0";
                                else temp = read_map[i].ButtonContent.ToLower();
                                ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] += temp + ",";
                                ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] += read_map[i].Length + ",";
                                ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] += ",";
                                ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] += i.ToString();
                            }
                            else ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] = "";

                        }
                        for (int i = read_map.Count; i < ModbusInfo.nTagNUM; i++)
                        {
                            ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, i] = "";
                        }
                    }
                }
            }
        }

        public static void SetWriteMap()
        {
            if (UCModbus.MainViewModel.CurrentNode != null)
            {
                var currentnode = UCModbus.MainViewModel.CurrentNode;
                if (UCModbus.MainViewModel.SubContent is UCDevice device)
                {
                    var write_map = device.viewModel.WriteMap;
                    if (currentnode.NodeType == NodeType.TCPDevice)
                    {
                        var nTCP = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(currentnode.ParentNode);
                        var nDevice_node = currentnode.ParentNode.Children.IndexOf(currentnode);
                        for (int i = 0; i < write_map.Count; i++)
                        {
                            if (write_map[i].IsComplete == true)
                            {

                                ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] = "";
                                if (write_map[i].Length > 1)
                                    ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] += Convert.ToString(write_map[i].Function + 10, 16).PadLeft(2, '0') + ",";
                                else
                                    ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] += (write_map[i].Function).ToString().PadLeft(2, '0') + ",";
                                ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] += write_map[i].TagAddress + ",";
                                ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] += write_map[i].Register + ",";
                                var temp = "";
                                if (write_map[i].ButtonContent == ModbusPart.Properties.Lang.Lang.Enable)
                                    temp = "1";
                                else if (write_map[i].ButtonContent == ModbusPart.Properties.Lang.Lang.Disable)
                                    temp = "0";
                                else temp = write_map[i].ButtonContent.ToLower();
                                ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] += temp + ",";
                                ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] += write_map[i].Length + ",";
                                ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] += ",";
                                ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] += i.ToString();
                            }
                            else ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] = "";
                        }
                        for (int i = write_map.Count; i < ModbusInfo.nTagNUM; i++)
                        {
                            ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, i] = "";
                        }
                    }
                    if (currentnode.NodeType == NodeType.SerialDevice)
                    {
                        var nSerial = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(currentnode.ParentNode);
                        var nDevice_node = currentnode.ParentNode.Children.IndexOf(currentnode);
                        for (int i = 0; i < write_map.Count; i++)
                        {
                            if (write_map[i].IsComplete == true)
                            {
                                ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] = "";
                                if (write_map[i].Length > 1)
                                    ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] += Convert.ToString(write_map[i].Function + 10, 16).PadLeft(2, '0') + ",";
                                else
                                    ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] += (write_map[i].Function).ToString().PadLeft(2, '0') + ",";
                                ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] += write_map[i].TagAddress + ",";
                                ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] += write_map[i].Register + ",";
                                var temp = "";
                                if (write_map[i].ButtonContent == ModbusPart.Properties.Lang.Lang.Enable)
                                    temp = "1";
                                else if (write_map[i].ButtonContent == ModbusPart.Properties.Lang.Lang.Disable)
                                    temp = "0";
                                else temp = write_map[i].ButtonContent.ToLower();
                                ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] += temp + ",";
                                ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] += write_map[i].Length + ",";
                                ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] += ",";
                                ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] += i.ToString();
                            }
                            else ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] = "";
                        }
                        for (int i = write_map.Count; i < ModbusInfo.nTagNUM; i++)
                        {
                            ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, i] = "";
                        }
                    }
                }
            }


        }

        /// <summary>
        /// IniData
        /// </summary>
        public static void LoadIniData()
        {
            #region ModbusInfo Code Load
            //初始化結構陣列
            ModbusInfo.Server_Station = 1;
            ModbusInfo.Server_TCP_Port = 502;
            ModbusInfo.TCP_Amount = 0;
            ModbusInfo.COM_Amount = 0;
            //最後一組拿來做DEFAULT值用

            for (int index = 0; index <= ModbusInfo.nSerialNUM; index++)
            {
                ModbusInfo.Serial[index].portName = "Modbus Serial";
                ModbusInfo.Serial[index].bEnable = false;
                ModbusInfo.Serial[index].COM = 1;
                ModbusInfo.Serial[index].Baudrate = 9600;
                ModbusInfo.Serial[index].DataBit = 8;
                ModbusInfo.Serial[index].Parity = 0;
                ModbusInfo.Serial[index].StopBit = 1;
                ModbusInfo.Serial[index].TimeOut = 100;
                ModbusInfo.Serial[index].Timer = 10;
                ModbusInfo.Serial[index].NUM = 0;
                ModbusInfo.Serial[index].Interface = 485;
                ModbusInfo.Serial[index].EnableSerialDevice = new bool[ModbusInfo.nDeviceNUM];
                ModbusInfo.Serial[index].Station = new int[ModbusInfo.nDeviceNUM];
                ModbusInfo.Serial[index].Error_reg = new int[ModbusInfo.nDeviceNUM];
                ModbusInfo.Serial[index].Device_Read_Limitlen = new int[ModbusInfo.nDeviceNUM];
                for (int j = 0; j < ModbusInfo.nDeviceNUM; j++)
                {
                    ModbusInfo.Serial[index].Error_reg[j] = -1; //特別初始化為-1，資料不能為0
                    ModbusInfo.Serial[index].Device_Read_Limitlen[j] = 100; //特別初始化為100，資料不能為0
                }
                ModbusInfo.Serial[index].strType = new string[ModbusInfo.nDeviceNUM];
                ModbusInfo.Serial[index].deviceName = new string[ModbusInfo.nDeviceNUM];

                ModbusInfo.Serial[index].Modbus_r = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                ModbusInfo.Serial[index].Modbus_w = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                for (int j = 0; j < ModbusInfo.Serial[index].Station.Length; j++)
                {
                    ModbusInfo.Serial[index].Station[j] = 0; //特別初始化為-1用以判斷是否是要回存的資料
                    ModbusInfo.Serial[index].strType[j] = "Device";
                }

                // TEMP
                ModbusInfo.Serial_Temp[index].portName = "Modbus Serial";
                ModbusInfo.Serial_Temp[index].bEnable = false;
                ModbusInfo.Serial_Temp[index].COM = 1;
                ModbusInfo.Serial_Temp[index].Baudrate = 9600;
                ModbusInfo.Serial_Temp[index].DataBit = 8;
                ModbusInfo.Serial_Temp[index].Parity = 0;
                ModbusInfo.Serial_Temp[index].StopBit = 1;
                ModbusInfo.Serial_Temp[index].TimeOut = 100;
                ModbusInfo.Serial_Temp[index].NUM = 0;
                ModbusInfo.Serial_Temp[index].Interface = 485;
                ModbusInfo.Serial_Temp[index].EnableSerialDevice = new bool[ModbusInfo.nDeviceNUM];
                ModbusInfo.Serial_Temp[index].Station = new int[ModbusInfo.nDeviceNUM];
                ModbusInfo.Serial_Temp[index].Error_reg = new int[ModbusInfo.nDeviceNUM];
                ModbusInfo.Serial_Temp[index].Device_Read_Limitlen = new int[ModbusInfo.nDeviceNUM];
                for (int j = 0; j < ModbusInfo.nDeviceNUM; j++)
                {
                    ModbusInfo.Serial_Temp[index].Error_reg[j] = -1; //特別初始化為-1，資料不能為0
                    ModbusInfo.Serial_Temp[index].Device_Read_Limitlen[j] = 100; //特別初始化為100，資料不能為0
                }
                ModbusInfo.Serial_Temp[index].strType = new string[ModbusInfo.nDeviceNUM];
                ModbusInfo.Serial_Temp[index].deviceName = new string[ModbusInfo.nDeviceNUM];

                ModbusInfo.Serial_Temp[index].Modbus_r = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                ModbusInfo.Serial_Temp[index].Modbus_w = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                for (int j = 0; j < ModbusInfo.Serial[index].Station.Length; j++)
                {
                    ModbusInfo.Serial_Temp[index].Station[j] = 0; //特別初始化為-1用以判斷是否是要回存的資料
                    ModbusInfo.Serial_Temp[index].strType[j] = "Device";
                }
            }

            //最後一組拿來做DEFAULT值用
            for (int index = 0; index <= ModbusInfo.nTCPNUM; index++)
            {
                ModbusInfo.TCP[index].bEnable = false;
                ModbusInfo.TCP[index].ip = "127.0.0.1";
                ModbusInfo.TCP[index].port = 502;
                ModbusInfo.TCP[index].Retry = 3;
                ModbusInfo.TCP[index].TimeOut = 100;
                ModbusInfo.TCP[index].Timer = 1;
                ModbusInfo.TCP[index].NUM = 0;
                ModbusInfo.TCP[index].TCPName = "Modbus TCP";

                ModbusInfo.TCP[index].EnableTCPDevice = new bool[ModbusInfo.nDeviceNUM];
                ModbusInfo.TCP[index].Station = new int[ModbusInfo.nDeviceNUM];
                ModbusInfo.TCP[index].strType = new string[ModbusInfo.nDeviceNUM];
                ModbusInfo.TCP[index].deviceName = new string[ModbusInfo.nDeviceNUM];
                ModbusInfo.TCP[index].Error_reg = new int[ModbusInfo.nDeviceNUM];
                ModbusInfo.TCP[index].Device_Read_Limitlen = new int[ModbusInfo.nDeviceNUM];
                for (int j = 0; j < ModbusInfo.nDeviceNUM; j++)
                {
                    ModbusInfo.TCP[index].Error_reg[j] = -1; //特別初始化為-1，資料不能為0
                    ModbusInfo.TCP[index].Device_Read_Limitlen[j] = 100; //特別初始化為100，資料不能為0
                }

                ModbusInfo.TCP[index].Modbus_r = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                ModbusInfo.TCP[index].Modbus_w = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];

                //TEMP
                ModbusInfo.TCP_Temp[index].bEnable = false;
                ModbusInfo.TCP_Temp[index].ip = "127.0.0.1";
                ModbusInfo.TCP_Temp[index].port = 502;
                ModbusInfo.TCP_Temp[index].Retry = 3;
                ModbusInfo.TCP_Temp[index].TimeOut = 100;
                ModbusInfo.TCP_Temp[index].NUM = 0;
                ModbusInfo.TCP_Temp[index].TCPName = "Modbus TCP";

                ModbusInfo.TCP_Temp[index].EnableTCPDevice = new bool[ModbusInfo.nDeviceNUM];
                ModbusInfo.TCP_Temp[index].Station = new int[ModbusInfo.nDeviceNUM];
                ModbusInfo.TCP_Temp[index].Error_reg = new int[ModbusInfo.nDeviceNUM];
                ModbusInfo.TCP_Temp[index].Device_Read_Limitlen = new int[ModbusInfo.nDeviceNUM];
                for (int j = 0; j < ModbusInfo.nDeviceNUM; j++)
                {
                    ModbusInfo.TCP_Temp[index].Error_reg[j] = -1; //特別初始化為-1，資料不能為0
                    ModbusInfo.TCP_Temp[index].Device_Read_Limitlen[j] = 100; //特別初始化為100，資料不能為0
                }
                ModbusInfo.TCP_Temp[index].strType = new string[ModbusInfo.nDeviceNUM];
                ModbusInfo.TCP_Temp[index].deviceName = new string[ModbusInfo.nDeviceNUM];

                ModbusInfo.TCP_Temp[index].Modbus_r = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                ModbusInfo.TCP_Temp[index].Modbus_w = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
            }

            //最後一組拿來做DEFAULT值用
            for (int i = 0; i <= ModbusInfo.nSerialNUM; i++)
            {
                for (int j = 0; j < ModbusInfo.nDeviceNUM; j++)
                {
                    for (int k = 0; k < ModbusInfo.nTagNUM; k++)
                    {
                        ModbusInfo.Serial[i].Modbus_r[j, k] = "";
                        ModbusInfo.Serial[i].Modbus_w[j, k] = "";

                        ModbusInfo.Serial_Temp[i].Modbus_r[j, k] = "";
                        ModbusInfo.Serial_Temp[i].Modbus_w[j, k] = "";
                    }
                }
            }

            //最後一組拿來做DEFAULT值用
            for (int i = 0; i <= ModbusInfo.nTCPNUM; i++)
            {
                for (int j = 0; j < ModbusInfo.nDeviceNUM; j++)
                {
                    for (int k = 0; k < ModbusInfo.nTagNUM; k++)
                    {
                        ModbusInfo.TCP[i].Modbus_r[j, k] = "";
                        ModbusInfo.TCP[i].Modbus_w[j, k] = "";

                        ModbusInfo.TCP_Temp[i].Modbus_r[j, k] = "";
                        ModbusInfo.TCP_Temp[i].Modbus_w[j, k] = "";
                    }
                }
            }


            //Load ModbusInfo.ini
            if (-1 != Class_InifileRW.Loadini())
            {
                try
                {
                    ModbusInfo.ini_Version = ModbusInfo.Current_ini_Version;

                    ModbusInfo.Server_Station = (int)Class_InifileRW.ini_ReadInteger("Config", "Station", 1);
                    ModbusInfo.Server_TCP_Port = (int)Class_InifileRW.ini_ReadInteger("Config", "TCPport", 502);

                    #region Serial loading
                    ModbusInfo.COM_Amount = (int)Class_InifileRW.ini_ReadInteger("Config", "COMNUM", 0);
                    for (int nSerial = 0; nSerial < ModbusInfo.COM_Amount; nSerial++)
                    {

                        ModbusInfo.Serial[nSerial].bEnable = (Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "Enable", 0) == 1) ? true : false;
                        ModbusInfo.Serial[nSerial].portName = Class_InifileRW.GetIniDataToCString("SERIAL" + (nSerial + 1).ToString(), "name", "");
                        ModbusInfo.Serial[nSerial].COM = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "COM", 0);
                        if (Class_InifileRW.GetIniDataToCString("SERIAL" + (nSerial + 1).ToString(), "RTUASC", "") != "")
                            ModbusInfo.Serial[nSerial].RTUASC = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "RTUASC", 0);
                        ModbusInfo.Serial[nSerial].Interface = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "Interface", 0);
                        ModbusInfo.Serial[nSerial].Baudrate = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "Baudrate", 0);
                        ModbusInfo.Serial[nSerial].Parity = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "Parity", 0);
                        ModbusInfo.Serial[nSerial].DataBit = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "DataBit", 0);
                        ModbusInfo.Serial[nSerial].StopBit = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "StopBit", 0);
                        ModbusInfo.Serial[nSerial].Retry = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "Retry", 0);
                        ModbusInfo.Serial[nSerial].TimeOut = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "TimeOut", 0);
                        ModbusInfo.Serial[nSerial].Timer = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "Timer", 0);
                        if (ModbusInfo.Serial[nSerial].Timer <= 10)
                            ModbusInfo.Serial[nSerial].Timer = 10;
                        ModbusInfo.Serial[nSerial].NUM = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString(), "NUM", 0);
                        for (int nDevice_node = 0; nDevice_node < ModbusInfo.Serial[nSerial].NUM; nDevice_node++)
                        {
                            ModbusInfo.Serial[nSerial].EnableSerialDevice[nDevice_node] = (Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Enable", 0) == 1) ? true : false;
                            ModbusInfo.Serial[nSerial].deviceName[nDevice_node] = Class_InifileRW.GetIniDataToCString("SERIAL" + (nSerial + 1).ToString() + "." + (nDevice_node + 1).ToString(), "name", "");
                            ModbusInfo.Serial[nSerial].Station[nDevice_node] = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Station", 0);
                            ModbusInfo.Serial[nSerial].Error_reg[nDevice_node] = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Error", 0);
                            ModbusInfo.Serial[nSerial].Device_Read_Limitlen[nDevice_node] = (int)Class_InifileRW.ini_ReadInteger("SERIAL" + (nSerial + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Device_Read_Limitlen", 0);
                            if (ModbusInfo.Serial[nSerial].Device_Read_Limitlen[nDevice_node] < 1)
                                ModbusInfo.Serial[nSerial].Device_Read_Limitlen[nDevice_node] = 100;
                            ModbusInfo.Serial[nSerial].strType[nDevice_node] = Class_InifileRW.GetIniDataToCString("SERIAL" + (nSerial + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Type", "");

                            string[] FunCodeRW_ini = new string[7];
                            string[] FunCodeRW = new string[] { "", "", "", "", "", "", "" };
                            int cntR = 0, cntW = 0;
                            for (int i = 0; i < ModbusInfo.nTagNUM; i++)
                            {
                                for (int clean = 0; clean < FunCodeRW.Length; clean++)
                                {
                                    FunCodeRW[clean] = "";
                                }
                                FunCodeRW_ini = Class_InifileRW.GetIniDataToCString("SERIAL" + (nSerial + 1).ToString() + "." + (nDevice_node + 1).ToString(), (i + 1).ToString(), "0").Split(new char[] { ',' });
                                // 沒有資料
                                if (FunCodeRW_ini.Length < 5) break;
                                int idx = 0;
                                foreach (String content in FunCodeRW_ini)
                                {
                                    FunCodeRW[idx] = content;
                                    idx++;
                                }
                                if (FunCodeRW[0].Length == 0)
                                {
                                    // Function code empty
                                    WPFToolkit.Controls.ToolkitMessageBox.Show($"[SERIAL{nSerial + 1}.{nDevice_node + 1}]" + "Function code empty", "Ini file error", MessageBoxButton.OK, InfoType.Error,new WPFToolkit.Controls.ToolkitMessageBoxButtonText());
                                }
                                if (FunCodeRW[1].Length == 0)
                                {
                                    // Tag Address value empty
                                    WPFToolkit.Controls.ToolkitMessageBox.Show($"[SERIAL{nSerial + 1}.{nDevice_node + 1}]" + "Tag Address value empty", "Ini file error", MessageBoxButton.OK, InfoType.Error, new WPFToolkit.Controls.ToolkitMessageBoxButtonText());
                                }
                                if (FunCodeRW[2].Length == 0)
                                {
                                    // Register value empty

                                    WPFToolkit.Controls.ToolkitMessageBox.Show($"[SERIAL{nSerial + 1}.{nDevice_node + 1}]" + "Register value empty", "Ini file error", MessageBoxButton.OK, InfoType.Error, new WPFToolkit.Controls.ToolkitMessageBoxButtonText());
                                }
                                if (FunCodeRW[3].Length == 0)
                                {
                                    // Enable setting empty
                                    WPFToolkit.Controls.ToolkitMessageBox.Show($"[SERIAL{nSerial + 1}.{nDevice_node + 1}]" + "Enable setting empty", "Ini file error", MessageBoxButton.OK, InfoType.Error, new WPFToolkit.Controls.ToolkitMessageBoxButtonText());
                                }
                                int itemidx = 0;
                                switch (Convert.ToInt16(FunCodeRW[0], 16))
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                        if (Convert.ToDouble(ModbusInfo.ini_Version) == 1)
                                        {
                                            // Version 1, convert to new configure
                                            FunCodeRW[5] = FunCodeRW[4];
                                            FunCodeRW[4] = "1";
                                            FunCodeRW[6] = cntR.ToString();
                                        }
                                        else
                                        {
                                            // Version after 1.1
                                            if (FunCodeRW[4].Length == 0)
                                            {
                                                // Read length empty

                                                WPFToolkit.Controls.ToolkitMessageBox.Show($"[SERIAL{nSerial + 1}.{nDevice_node + 1}]" + "Read length empty", "Ini file error", MessageBoxButton.OK, InfoType.Error);
                                                continue;
                                            }
                                        }
                                        itemidx = Convert.ToInt16(FunCodeRW[6]);
                                        ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, itemidx] = "";
                                        ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt32(FunCodeRW[0], 16), 16).ToUpper().PadLeft(2, '0') + ",");
                                        ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt32(FunCodeRW[1], 16), 16).ToUpper().PadLeft(4, '0') + ",");
                                        ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt32(FunCodeRW[2]), 10) + ",");
                                        ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, itemidx] += FunCodeRW[3] + ",";
                                        ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[4]), 10) + ",");
                                        ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, itemidx] += FunCodeRW[5] + ",";
                                        ModbusInfo.Serial[nSerial].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[6]), 10));
                                        cntR++;
                                        break;
                                    case 5:
                                    case 6:
                                    case 15:
                                    case 16:
                                        if (Convert.ToDouble(ModbusInfo.ini_Version) == 1)
                                        {
                                            // Version 1, convert to new configure
                                            FunCodeRW[6] = cntW.ToString();
                                        }
                                        if (FunCodeRW[4].Length == 0)
                                        {
                                            // Read length empty
                                            WPFToolkit.Controls.ToolkitMessageBox.Show($"[SERIAL{nSerial + 1}.{nDevice_node + 1}]" + "Read length empty", "ini file error", MessageBoxButton.OK, InfoType.Error,new WPFToolkit.Controls.ToolkitMessageBoxButtonText());
                                            continue;
                                        }
                                        itemidx = Convert.ToInt16(FunCodeRW[6]);
                                        ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, itemidx] = "";
                                        ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[0], 16), 16).ToUpper().PadLeft(2, '0') + ",");
                                        ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt32(FunCodeRW[1], 16), 16).ToUpper().PadLeft(4, '0') + ",");
                                        ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt32(FunCodeRW[2]), 10) + ",");
                                        ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, itemidx] += FunCodeRW[3] + ",";
                                        ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[4]), 10) + ",");
                                        ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, itemidx] += FunCodeRW[5] + ",";
                                        ModbusInfo.Serial[nSerial].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[6]), 10));
                                        cntW++;
                                        break;
                                }
                            }
                        }
                    }
                    #endregion

                    #region TCP loading

                    ModbusInfo.TCP_Amount = (int)Class_InifileRW.ini_ReadInteger("Config", "TCPNUM", 0);
                    for (int nTCP = 0; nTCP < ModbusInfo.TCP_Amount; nTCP++)
                    {
                        ModbusInfo.TCP[nTCP].bEnable = (Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString(), "Enable", 0) == 1) ? true : false;
                        ModbusInfo.TCP[nTCP].TCPName = Class_InifileRW.GetIniDataToCString("TCP" + (nTCP + 1).ToString(), "name", "");
                        ModbusInfo.TCP[nTCP].ip = Class_InifileRW.GetIniDataToCString("TCP" + (nTCP + 1).ToString(), "IP", "");
                        ModbusInfo.TCP[nTCP].port = (int)Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString(), "Port", 0);
                        ModbusInfo.TCP[nTCP].Retry = (int)Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString(), "Retry", 0);
                        ModbusInfo.TCP[nTCP].TimeOut = (int)Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString(), "TimeOut", 0);
                        ModbusInfo.TCP[nTCP].Timer = (int)Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString(), "Timer", 0);
                        if (ModbusInfo.TCP[nTCP].Timer <= 0)
                            ModbusInfo.TCP[nTCP].Timer = 1;
                        ModbusInfo.TCP[nTCP].NUM = (int)Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString(), "Num", 0);//20181004修正讀取ini異常NUM>>Num
                        for (int nDevice_node = 0; nDevice_node < ModbusInfo.TCP[nTCP].NUM; nDevice_node++)
                        {
                            ModbusInfo.TCP[nTCP].EnableTCPDevice[nDevice_node] = (Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Enable", 0) == 1) ? true : false;
                            ModbusInfo.TCP[nTCP].deviceName[nDevice_node] = Class_InifileRW.GetIniDataToCString("TCP" + (nTCP + 1).ToString() + "." + (nDevice_node + 1).ToString(), "name", "");
                            ModbusInfo.TCP[nTCP].Station[nDevice_node] = (int)Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Station", 0);
                            ModbusInfo.TCP[nTCP].Error_reg[nDevice_node] = (int)Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Error", 0);
                            ModbusInfo.TCP[nTCP].Device_Read_Limitlen[nDevice_node] = (int)Class_InifileRW.ini_ReadInteger("TCP" + (nTCP + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Device_Read_Limitlen", 0);
                            if (ModbusInfo.TCP[nTCP].Device_Read_Limitlen[nDevice_node] < 1)
                                ModbusInfo.TCP[nTCP].Device_Read_Limitlen[nDevice_node] = 100;
                            ModbusInfo.TCP[nTCP].strType[nDevice_node] = Class_InifileRW.GetIniDataToCString("TCP" + (nTCP + 1).ToString() + "." + (nDevice_node + 1).ToString(), "Type", "");

                            string[] FunCodeRW_ini = new string[7];
                            string[] FunCodeRW = new string[] { "", "", "", "", "", "", "" };
                            int cntR = 0, cntW = 0;
                            for (int i = 0; i < ModbusInfo.nTagNUM; i++)
                            {
                                for (int clean = 0; clean < FunCodeRW.Length; clean++)
                                {
                                    FunCodeRW[clean] = "";
                                }
                                FunCodeRW_ini = Class_InifileRW.GetIniDataToCString("TCP" + (nTCP + 1).ToString() + "." + (nDevice_node + 1).ToString(), (i + 1).ToString(), "").Split(new char[] { ',' });
                                // 沒有資料
                                if (FunCodeRW_ini.Length < 5) break;
                                int idx = 0;
                                foreach (String content in FunCodeRW_ini)
                                {
                                    FunCodeRW[idx] = content;
                                    idx++;
                                }
                                if (FunCodeRW[0].Length == 0)
                                {
                                    // Function code empty
                                    WPFToolkit.Controls.ToolkitMessageBox.Show($"[TCP{nTCP + 1}.{nDevice_node + 1}]" + "Function code empty", "Ini file error", MessageBoxButton.OK, InfoType.Error);
                                }
                                if (FunCodeRW[1].Length == 0)
                                {
                                    // Tag Address value empty
                                    WPFToolkit.Controls.ToolkitMessageBox.Show($"[TCP{nTCP + 1}.{nDevice_node + 1}]" + "Tag Address value empty", "Ini file error", MessageBoxButton.OK, InfoType.Error);
                                }
                                if (FunCodeRW[2].Length == 0)
                                {
                                    // Register value empty
                                    WPFToolkit.Controls.ToolkitMessageBox.Show($"[TCP{nTCP + 1}.{nDevice_node + 1}]" + "Register value empty", "Ini file error", MessageBoxButton.OK, InfoType.Error);
                                }
                                if (FunCodeRW[3].Length == 0)
                                {
                                    // Enable setting empty
                                    WPFToolkit.Controls.ToolkitMessageBox.Show($"[TCP{nTCP + 1}.{nDevice_node + 1}]" + "Enable setting empty", "Ini file error", MessageBoxButton.OK, InfoType.Error);
                                }
                                int itemidx = 0;
                                switch (Convert.ToInt16(FunCodeRW[0], 16))
                                {
                                    case 1:
                                    case 2:
                                    case 3:
                                    case 4:
                                        if (Convert.ToDouble(ModbusInfo.ini_Version) == 1)
                                        {
                                            // Version 1, convert to new configure
                                            FunCodeRW[5] = FunCodeRW[4];
                                            FunCodeRW[4] = "1";
                                            FunCodeRW[6] = cntR.ToString();
                                        }
                                        else
                                        {
                                            // Version after 1.1
                                            if (FunCodeRW[4].Length == 0)
                                            {
                                                // Read length empty
                                                WPFToolkit.Controls.ToolkitMessageBox.Show($"[TCP{nTCP + 1}.{nDevice_node + 1}]" + "Read length empty", "Ini file error", MessageBoxButton.OK, InfoType.Error);
                                                continue;
                                            }
                                        }
                                        itemidx = Convert.ToInt16(FunCodeRW[6]);
                                        ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, itemidx] = "";
                                        ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[0], 16), 16).ToUpper().PadLeft(2, '0') + ",");
                                        ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt32(FunCodeRW[1], 16), 16).ToUpper().PadLeft(4, '0') + ",");
                                        ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt32(FunCodeRW[2]), 10) + ",");
                                        ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, itemidx] += FunCodeRW[3] + ",";
                                        ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[4]), 10) + ",");
                                        ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, itemidx] += FunCodeRW[5] + ",";
                                        ModbusInfo.TCP[nTCP].Modbus_r[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[6]), 10));
                                        cntR++;
                                        break;
                                    case 5:
                                    case 6:
                                    case 15:
                                    case 16:
                                        if (Convert.ToDouble(ModbusInfo.ini_Version) == 1)
                                        {
                                            // Version 1, convert to new configure
                                            FunCodeRW[6] = cntW.ToString();
                                        }
                                        if (FunCodeRW[4].Length == 0)
                                        {
                                            // Read length empty
                                            WPFToolkit.Controls.ToolkitMessageBox.Show($"[TCP{nTCP + 1}.{nDevice_node + 1}]" + "Read length empty", "Ini file error", MessageBoxButton.OK, InfoType.Error);
                                            continue;
                                        }
                                        itemidx = Convert.ToInt16(FunCodeRW[6]);
                                        ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, itemidx] = "";
                                        ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[0], 16), 16).ToUpper().PadLeft(2, '0') + ",");
                                        ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt32(FunCodeRW[1], 16), 16).ToUpper().PadLeft(4, '0') + ",");
                                        ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt32(FunCodeRW[2]), 10) + ",");
                                        ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, itemidx] += FunCodeRW[3] + ",";
                                        ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[4]), 10) + ",");
                                        ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, itemidx] += FunCodeRW[5] + ",";
                                        ModbusInfo.TCP[nTCP].Modbus_w[nDevice_node, itemidx] += (Convert.ToString(Convert.ToInt16(FunCodeRW[6]), 10));
                                        cntW++;
                                        break;
                                }
                            }
                        }// END of for loop (Tag NUM)
                    } // END of for loop (TCP NUM)
                    #endregion
                }
                catch (Exception ex)
                {
                    WPFToolkit.Controls.ToolkitMessageBox.Show(ex.Message.ToString(), "System Error", MessageBoxButton.OK, InfoType.Error);
                }


            }
            #endregion

        }

        /// <summary>
        /// 保存數據
        /// </summary>
        public static void SaveFile()
        {
            // 檢查記憶體
            try
            {

                string path = Class_InifileRW.Current_ini_File_Path;
                string iniResult = "";
                iniResult = iniResult + "\r\n";
                iniResult = iniResult + "[Config]" + "\r\n";
                iniResult = iniResult + "Version=" + ModbusInfo.Current_ini_Version + "\r\n";
                iniResult = iniResult + "Station=" + ModbusInfo.Server_Station + "\r\n";
                iniResult = iniResult + "TCPport=" + ModbusInfo.Server_TCP_Port + "\r\n";
                iniResult = iniResult + "COMNUM=" + ModbusInfo.COM_Amount.ToString() + "\r\n";
                iniResult = iniResult + "TCPNUM=" + ModbusInfo.TCP_Amount.ToString() + "\r\n";
                iniResult = iniResult + "\r\n";

                #region Save TCP data
                for (int nTCP = 0; nTCP < ModbusInfo.TCP_Amount; nTCP++)
                {
                    iniResult = iniResult + "[TCP" + (nTCP + 1).ToString() + "]\r\n";
                    iniResult = iniResult + "name=" + ModbusInfo.TCP[nTCP].TCPName + "\r\n";
                    iniResult = iniResult + "Enable=" + Convert.ToUInt16(ModbusInfo.TCP[nTCP].bEnable) + "\r\n";
                    iniResult = iniResult + "IP=" + ModbusInfo.TCP[nTCP].ip.ToString() + "\r\n";
                    iniResult = iniResult + "Port=" + ModbusInfo.TCP[nTCP].port.ToString() + "\r\n";
                    iniResult = iniResult + "Retry=" + ModbusInfo.TCP[nTCP].Retry.ToString() + "\r\n";
                    iniResult = iniResult + "TimeOut=" + ModbusInfo.TCP[nTCP].TimeOut.ToString() + "\r\n";
                    iniResult = iniResult + "Timer=" + ModbusInfo.TCP[nTCP].Timer.ToString() + "\r\n";
                    iniResult = iniResult + "Num=" + ModbusInfo.TCP[nTCP].NUM.ToString() + "\r\n";
                    iniResult = iniResult + "\r\n";

                    for (int nDevice_node = 0; nDevice_node < ModbusInfo.TCP[nTCP].NUM; nDevice_node++)
                    {
                        string[,] r = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                        string[,] w = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                        iniResult = iniResult + "[TCP" + (nTCP + 1).ToString() + "." + (nDevice_node + 1).ToString() + "]\r\n";
                        iniResult = iniResult + "name=" + ModbusInfo.TCP[nTCP].deviceName[nDevice_node] + "\r\n";
                        iniResult = iniResult + "Enable=" + Convert.ToUInt16(ModbusInfo.TCP[nTCP].EnableTCPDevice[nDevice_node]) + "\r\n";
                        iniResult = iniResult + "Station=" + ModbusInfo.TCP[nTCP].Station[nDevice_node].ToString() + "\r\n";
                        iniResult = iniResult + "Error=" + ModbusInfo.TCP[nTCP].Error_reg[nDevice_node].ToString() + "\r\n";
                        iniResult = iniResult + "Device_Read_Limitlen=" + ModbusInfo.TCP[nTCP].Device_Read_Limitlen[nDevice_node].ToString() + "\r\n";
                        iniResult = iniResult + "Type=" + ModbusInfo.TCP[nTCP].strType[nDevice_node] + "\r\n";

                        r = ModbusInfo.TCP[nTCP].Modbus_r;
                        w = ModbusInfo.TCP[nTCP].Modbus_w;
                        List<TallItem> list_tallItems = new List<TallItem>();
                        int cntR = 1;
                        for (int fun_idx = 1; fun_idx <= 16; fun_idx++)
                        {
                            #region 資料排序
                            list_tallItems.Clear();
                            for (int i = 0; i < ModbusInfo.nTagNUM; i++)
                            {
                                if (fun_idx >= 1 && fun_idx <= 4)
                                {
                                    if (r[nDevice_node, i].Length == 0) continue;
                                    // 掃描功能碼 1~4
                                    if (Convert.ToInt32(r[nDevice_node, i].Substring(0, 2), 16) != fun_idx) continue;
                                    list_tallItems.Add(new TallItem() { STR = r[nDevice_node, i], REG = Convert.ToInt32(r[nDevice_node, i].Substring(3, 4), 16) });
                                }
                                else if (fun_idx >= 5 && fun_idx <= 6)
                                {
                                    if (w[nDevice_node, i].Length == 0) continue;
                                    // 掃描功能碼 5~6
                                    if (Convert.ToInt32(w[nDevice_node, i].Substring(0, 2), 16) != fun_idx) continue;
                                    list_tallItems.Add(new TallItem() { STR = w[nDevice_node, i], REG = Convert.ToInt32(w[nDevice_node, i].Substring(3, 4), 16) });
                                }
                                else if (fun_idx >= 15 && fun_idx <= 16)
                                {
                                    if (w[nDevice_node, i].Length == 0) continue;
                                    // 掃描功能碼 15~16
                                    if (Convert.ToInt32(w[nDevice_node, i].Substring(0, 2), 16) != fun_idx) continue;
                                    list_tallItems.Add(new TallItem() { STR = w[nDevice_node, i], REG = Convert.ToInt32(w[nDevice_node, i].Substring(3, 4), 16) });
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            list_tallItems.Sort((x, y) => { return x.REG.CompareTo(y.REG); });
                            #endregion
                            for (int j = 0; j < list_tallItems.Count; j++)
                            {
                                if (list_tallItems[j].STR.Length != 0)
                                    iniResult = iniResult + (cntR + j) + "=" + list_tallItems[j].STR + "\r\n";
                                else continue;
                            }
                            cntR += list_tallItems.Count;
                        }
                        iniResult = iniResult + "\r\n";
                    }

                }
                #endregion

                #region Save Serail data
                for (int nSerial = 0; nSerial < ModbusInfo.COM_Amount; nSerial++)
                {
                    iniResult = iniResult + "[SERIAL" + (nSerial + 1).ToString() + "]\r\n";
                    iniResult = iniResult + "name=" + ModbusInfo.Serial[nSerial].portName + "\r\n";
                    iniResult = iniResult + "Enable=" + Convert.ToUInt16(ModbusInfo.Serial[nSerial].bEnable) + "\r\n";
                    iniResult = iniResult + "COM=" + ModbusInfo.Serial[nSerial].COM.ToString() + "\r\n";
                    iniResult = iniResult + "RTUASC=" + ModbusInfo.Serial[nSerial].RTUASC.ToString() + "\r\n";
                    iniResult = iniResult + "Interface=" + ModbusInfo.Serial[nSerial].Interface.ToString() + "\r\n";
                    iniResult = iniResult + "Baudrate=" + ModbusInfo.Serial[nSerial].Baudrate.ToString() + "\r\n";
                    iniResult = iniResult + "Parity=" + ModbusInfo.Serial[nSerial].Parity.ToString() + "\r\n";
                    iniResult = iniResult + "DataBit=" + ModbusInfo.Serial[nSerial].DataBit.ToString() + "\r\n";
                    iniResult = iniResult + "StopBit=" + ModbusInfo.Serial[nSerial].StopBit.ToString() + "\r\n";
                    iniResult = iniResult + "Retry=" + ModbusInfo.Serial[nSerial].Retry.ToString() + "\r\n";
                    iniResult = iniResult + "TimeOut=" + ModbusInfo.Serial[nSerial].TimeOut.ToString() + "\r\n";
                    iniResult = iniResult + "Timer=" + ModbusInfo.Serial[nSerial].Timer.ToString() + "\r\n";
                    iniResult = iniResult + "NUM=" + ModbusInfo.Serial[nSerial].NUM.ToString() + "\r\n";
                    iniResult = iniResult + "\r\n";
                    if (ModbusInfo.Serial[nSerial].Interface == 232) //若Interface為232則儲存時僅保留第一站
                    {
                        ModbusInfo.Serial[nSerial].NUM = 1;
                    }
                    for (int nDevice_node = 0; nDevice_node < ModbusInfo.Serial[nSerial].NUM; nDevice_node++)
                    {
                        if (ModbusInfo.Serial[nSerial].Station[nDevice_node] == -1) break;
                        string[,] r = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                        string[,] w = new string[ModbusInfo.nDeviceNUM, ModbusInfo.nTagNUM];
                        iniResult = iniResult + "[SERIAL" + (nSerial + 1).ToString() + "." + (nDevice_node + 1).ToString() + "]\r\n";
                        iniResult = iniResult + "name=" + ModbusInfo.Serial[nSerial].deviceName[nDevice_node] + "\r\n";
                        iniResult = iniResult + "Enable=" + Convert.ToUInt16(ModbusInfo.Serial[nSerial].EnableSerialDevice[nDevice_node]) + "\r\n";
                        iniResult = iniResult + "Station=" + ModbusInfo.Serial[nSerial].Station[nDevice_node].ToString() + "\r\n";
                        iniResult = iniResult + "Error=" + ModbusInfo.Serial[nSerial].Error_reg[nDevice_node].ToString() + "\r\n";
                        iniResult = iniResult + "Device_Read_Limitlen=" + ModbusInfo.Serial[nSerial].Device_Read_Limitlen[nDevice_node].ToString() + "\r\n";
                        iniResult = iniResult + "Type=" + ModbusInfo.Serial[nSerial].strType[nDevice_node] + "\r\n";

                        r = ModbusInfo.Serial[nSerial].Modbus_r;
                        w = ModbusInfo.Serial[nSerial].Modbus_w;

                        List<TallItem> list_tallItems = new List<TallItem>();
                        int cntR = 1;
                        for (int fun_idx = 1; fun_idx <= 16; fun_idx++)
                        {
                            #region 資料排序
                            list_tallItems.Clear();
                            for (int i = 0; i < ModbusInfo.nTagNUM; i++)
                            {
                                if (fun_idx >= 1 && fun_idx <= 4)
                                {
                                    if (r[nDevice_node, i].Length == 0) continue;
                                    // 掃描功能碼 1~4
                                    if (Convert.ToInt32(r[nDevice_node, i].Substring(0, 2), 16) != fun_idx) continue;
                                    list_tallItems.Add(new TallItem() { STR = r[nDevice_node, i], REG = Convert.ToInt32(r[nDevice_node, i].Substring(3, 4), 16) });
                                }
                                else if (fun_idx >= 5 && fun_idx <= 6)
                                {
                                    if (w[nDevice_node, i].Length == 0) continue;
                                    // 掃描功能碼 5~6
                                    if (Convert.ToInt32(w[nDevice_node, i].Substring(0, 2), 16) != fun_idx) continue;
                                    list_tallItems.Add(new TallItem() { STR = w[nDevice_node, i], REG = Convert.ToInt32(w[nDevice_node, i].Substring(3, 4), 16) });
                                }
                                else if (fun_idx >= 15 && fun_idx <= 16)
                                {
                                    if (w[nDevice_node, i].Length == 0) continue;
                                    // 掃描功能碼 15~16
                                    if (Convert.ToInt32(w[nDevice_node, i].Substring(0, 2), 16) != fun_idx) continue;
                                    list_tallItems.Add(new TallItem() { STR = w[nDevice_node, i], REG = Convert.ToInt32(w[nDevice_node, i].Substring(3, 4), 16) });
                                }
                            }
                            // list_tallItems.Sort((x, y) => { return x.REG.CompareTo(y.REG); });
                            #endregion
                            for (int j = 0; j < list_tallItems.Count; j++)
                            {
                                if (list_tallItems[j].STR.Length != 0)
                                    iniResult = iniResult + (cntR + j) + "=" + list_tallItems[j].STR + "\r\n";
                                else continue;
                            }
                            cntR += list_tallItems.Count;
                        }
                        iniResult = iniResult + "\r\n";
                    }
                }
                #endregion
                System.IO.File.WriteAllText(path, iniResult);

                // 備份記憶體資料，做為關閉前的存檔判斷


                for (int IDX_ser = 0; IDX_ser < ModbusInfo.nSerialNUM; IDX_ser++)
                {
                    ModbusInfo.Serial_Temp[IDX_ser].portName = ModbusInfo.Serial[IDX_ser].portName;
                    ModbusInfo.Serial_Temp[IDX_ser].bEnable = ModbusInfo.Serial[IDX_ser].bEnable;
                    ModbusInfo.Serial_Temp[IDX_ser].COM = ModbusInfo.Serial[IDX_ser].COM;
                    ModbusInfo.Serial_Temp[IDX_ser].RTUASC = ModbusInfo.Serial[IDX_ser].RTUASC;
                    ModbusInfo.Serial_Temp[IDX_ser].Baudrate = ModbusInfo.Serial[IDX_ser].Baudrate;
                    ModbusInfo.Serial_Temp[IDX_ser].Parity = ModbusInfo.Serial[IDX_ser].Parity;
                    ModbusInfo.Serial_Temp[IDX_ser].DataBit = ModbusInfo.Serial[IDX_ser].DataBit;
                    ModbusInfo.Serial_Temp[IDX_ser].StopBit = ModbusInfo.Serial[IDX_ser].StopBit;
                    ModbusInfo.Serial_Temp[IDX_ser].Retry = ModbusInfo.Serial[IDX_ser].Retry;
                    ModbusInfo.Serial_Temp[IDX_ser].TimeOut = ModbusInfo.Serial[IDX_ser].TimeOut;
                    ModbusInfo.Serial_Temp[IDX_ser].Timer = ModbusInfo.Serial[IDX_ser].Timer;
                    ModbusInfo.Serial_Temp[IDX_ser].Interface = ModbusInfo.Serial[IDX_ser].Interface;
                    ModbusInfo.Serial_Temp[IDX_ser].NUM = ModbusInfo.Serial[IDX_ser].NUM;
                    for (int IDX_Device = 0; IDX_Device < ModbusInfo.nDeviceNUM; IDX_Device++)
                    {
                        ModbusInfo.Serial_Temp[IDX_ser].deviceName[IDX_Device] = ModbusInfo.Serial[IDX_ser].deviceName[IDX_Device];
                        ModbusInfo.Serial_Temp[IDX_ser].EnableSerialDevice[IDX_Device] = ModbusInfo.Serial[IDX_ser].EnableSerialDevice[IDX_Device];
                        ModbusInfo.Serial_Temp[IDX_ser].Station[IDX_Device] = ModbusInfo.Serial[IDX_ser].Station[IDX_Device];
                        ModbusInfo.Serial_Temp[IDX_ser].Error_reg[IDX_Device] = ModbusInfo.Serial[IDX_ser].Error_reg[IDX_Device];
                        ModbusInfo.Serial_Temp[IDX_ser].Device_Read_Limitlen[IDX_Device] = ModbusInfo.Serial[IDX_ser].Device_Read_Limitlen[IDX_Device];
                        ModbusInfo.Serial_Temp[IDX_ser].strType[IDX_Device] = ModbusInfo.Serial[IDX_ser].strType[IDX_Device];
                        for (int IDX_Tag = 0; IDX_Tag < ModbusInfo.nTagNUM; IDX_Tag++)
                        {
                            ModbusInfo.Serial_Temp[IDX_ser].Modbus_w[IDX_Device, IDX_Tag] = ModbusInfo.Serial[IDX_ser].Modbus_w[IDX_Device, IDX_Tag];
                            ModbusInfo.Serial_Temp[IDX_ser].Modbus_r[IDX_Device, IDX_Tag] = ModbusInfo.Serial[IDX_ser].Modbus_r[IDX_Device, IDX_Tag];
                        }
                    }
                }

                for (int IDX_ser = 0; IDX_ser < ModbusInfo.nTCPNUM; IDX_ser++)
                {
                    ModbusInfo.TCP_Temp[IDX_ser].TCPName = ModbusInfo.TCP[IDX_ser].TCPName;
                    ModbusInfo.TCP_Temp[IDX_ser].bEnable = ModbusInfo.TCP[IDX_ser].bEnable;
                    ModbusInfo.TCP_Temp[IDX_ser].ip = ModbusInfo.TCP[IDX_ser].ip;
                    ModbusInfo.TCP_Temp[IDX_ser].port = ModbusInfo.TCP[IDX_ser].port;
                    ModbusInfo.TCP_Temp[IDX_ser].Retry = ModbusInfo.TCP[IDX_ser].Retry;
                    ModbusInfo.TCP_Temp[IDX_ser].TimeOut = ModbusInfo.TCP[IDX_ser].TimeOut;
                    ModbusInfo.TCP_Temp[IDX_ser].Timer = ModbusInfo.TCP[IDX_ser].Timer;
                    ModbusInfo.TCP_Temp[IDX_ser].NUM = ModbusInfo.TCP[IDX_ser].NUM;
                    for (int IDX_Device = 0; IDX_Device < ModbusInfo.nDeviceNUM; IDX_Device++)
                    {
                        ModbusInfo.TCP_Temp[IDX_ser].deviceName[IDX_Device] = ModbusInfo.TCP[IDX_ser].deviceName[IDX_Device];
                        ModbusInfo.TCP_Temp[IDX_ser].EnableTCPDevice[IDX_Device] = ModbusInfo.TCP[IDX_ser].EnableTCPDevice[IDX_Device];
                        ModbusInfo.TCP_Temp[IDX_ser].Station[IDX_Device] = ModbusInfo.TCP[IDX_ser].Station[IDX_Device];
                        ModbusInfo.TCP_Temp[IDX_ser].Error_reg[IDX_Device] = ModbusInfo.TCP[IDX_ser].Error_reg[IDX_Device];
                        ModbusInfo.TCP_Temp[IDX_ser].Device_Read_Limitlen[IDX_Device] = ModbusInfo.TCP[IDX_ser].Device_Read_Limitlen[IDX_Device];
                        ModbusInfo.TCP_Temp[IDX_ser].strType[IDX_Device] = ModbusInfo.TCP[IDX_ser].strType[IDX_Device];
                        for (int IDX_Tag = 0; IDX_Tag < ModbusInfo.nTagNUM; IDX_Tag++)
                        {
                            ModbusInfo.TCP_Temp[IDX_ser].Modbus_w[IDX_Device, IDX_Tag] = ModbusInfo.TCP[IDX_ser].Modbus_w[IDX_Device, IDX_Tag];
                            ModbusInfo.TCP_Temp[IDX_ser].Modbus_r[IDX_Device, IDX_Tag] = ModbusInfo.TCP[IDX_ser].Modbus_r[IDX_Device, IDX_Tag];
                        }
                    }
                }
            }
            catch
            {
                WPFToolkit.Controls.ToolkitMessageBox.Show("File Save Failed!", "Fail", MessageBoxButton.OK, InfoType.Error);
            }
        }

    }
}

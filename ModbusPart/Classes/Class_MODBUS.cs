using System;

namespace CNC_SNC_CSharp
{
    public class ModbusInfo
    {
        public static readonly int nTCPNUM = 50;
        public static readonly int nSerialNUM = 8;
        public static readonly int nDeviceNUM = 256;
        public static readonly int nTagNUM = 512;

        public static String Current_ini_Version = "1.1";
        public static String ini_Version;
        public static int Server_Station = 1;
        public static int Server_TCP_Port = 502;
        public static int COM_Amount = 0;
        public static int TCP_Amount = 0;

        public struct ModbusSerial
        {
            public string portName;
            public bool bEnable;
            public int COM;
            public int RTUASC;
            public int Baudrate;
            public int Parity;
            public int DataBit;
            public int StopBit;
            public int Retry;
            public int TimeOut;
            public int Interface;
            public int Timer;
            public int NUM;
            public string[] deviceName;
            public bool[] EnableSerialDevice;
            public int[] Station;
            public int[] Error_reg;
            public int[] Device_Read_Limitlen;
            public string[] strType;
            public string[,] Modbus_w;
            public string[,] Modbus_r;
        }

        public struct ModbusTCP
        {
            public string TCPName;
            public bool bEnable;
            public string ip;
            public int port;
            public int Retry;
            public int TimeOut;
            public int Timer;
            public int NUM;
            public bool[] EnableTCPDevice;
            public string[] deviceName;
            public int[] Station;
            public int[] Error_reg;
            public int[] Device_Read_Limitlen;
            public string[] strType;
            public string[,] Modbus_w;
            public string[,] Modbus_r;
        }

        public static ModbusSerial[] Serial = new ModbusSerial[nSerialNUM + 1]; // 多一組給刪除時用
        public static ModbusTCP[] TCP = new ModbusTCP[nTCPNUM + 1]; // 多一組給刪除時用

        //Form 結束時檢查資料是否有被更動時用
        public static ModbusSerial[] Serial_Temp = new ModbusSerial[nSerialNUM + 1]; // 多一組給刪除時用
        public static ModbusTCP[] TCP_Temp = new ModbusTCP[nTCPNUM + 1]; // 多一組給刪除時用

        public static readonly string[] DefaultVFD =  /*** VFD ***/
        {
            "2000,06,-1",
            "2001,06,-1",
            "2002,06,-1",
            "2100,03,-1",
            "2101,03,-1",
            "2102,03,-1",
            "2103,03,-1",
            "2104,03,-1",
            "2105,03,-1",
            "2106,03,-1",
            "2107,03,-1",
            "2108,03,-1",
            "2109,03,-1",
            "210A,03,-1",
            "210B,03,-1",
            "210C,03,-1",
            "210D,03,-1",
            "210E,03,-1",
            "210F,03,-1",
            "2110,03,-1",
            "2200,03,-1",
            "2201,03,-1",
            "2202,03,-1",
            "2203,03,-1",
            "2204,03,-1",
            "2205,03,-1",
            "2206,03,-1"
        };

        public static readonly string[] DefaultDMV =
        {
            "1000,06,-1",
            "1001,06,-1",
            "10B0,06,-1",
            "10B1,06,-1",
            "1010,03,-1",
            "1011,03,-1",
            "1012,03,-1",
            "1013,03,-1",
            "1014,03,-1",
            "1015,03,-1",
            "1016,03,-1",
            "1017,03,-1",
            "1018,03,-1",
            "1019,03,-1",
            "101A,03,-1",
            "101B,03,-1",
            "101C,03,-1",
            "101D,03,-1",
            "101E,03,-1",
            "101F,03,-1",
            "1020,03,-1",
            "1021,03,-1",
            "1022,03,-1",
            "1023,03,-1",
            "1024,03,-1",
            "1025,03,-1",
            "1026,03,-1",
            "1027,03,-1",
            "1028,03,-1",
            "1029,03,-1",
            "102A,03,-1",
            "102B,03,-1",
            "102C,03,-1",
            "102D,03,-1",
            "102E,03,-1",
            "102F,03,-1",
            "1030,03,-1",
            "1031,03,-1",
            "1032,03,-1",
            "1033,03,-1",
            "1034,03,-1",
            "1035,03,-1",
            "1036,03,-1",
            "1037,03,-1",
            "1038,03,-1",
            "1039,03,-1",
            "103A,03,-1",
            "103B,03,-1",
            "103C,03,-1",
            "103D,03,-1",
            "103E,03,-1",
            "103F,03,-1",
            "1040,03,-1",
            "1041,03,-1",
            "1042,03,-1",
            "1043,03,-1",
            "1044,03,-1",
            "1045,03,-1",
            "1046,03,-1",
            "1047,03,-1",
            "1048,03,-1",
            "1049,03,-1",
            "104A,03,-1",
            "104B,03,-1",
            "104C,03,-1",
            "104D,03,-1",
            "104E,03,-1",
            "104F,03,-1",
            "1060,03,-1"
        };
    }
}




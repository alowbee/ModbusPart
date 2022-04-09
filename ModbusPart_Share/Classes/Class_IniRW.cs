using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using WPFToolkit.Controls;
using WPFToolkit.Data.Enum;

namespace EndUser
{
    public class Class_IniRW
    {
        [DllImport("kernel32", SetLastError = true, EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileString(byte[] section, byte[] key, byte[] def, byte[] retVal, int size, string filePath);
        [DllImport("kernel32", EntryPoint = "WritePrivateProfileString")]
        private static extern bool WritePrivateProfileString(byte[] section, byte[] key, byte[] val, string filepath);
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileSection", SetLastError = true)]
        static extern uint WritePrivateProfileSection(byte[] section, byte[] lpString, string filePath);


        //与ini交互必须统一编码格式
        private static byte[] getBytes(string s, string encodingName)
        {
            return null == s ? null : Encoding.GetEncoding(encodingName).GetBytes(s);
        }

        public static string GetIniDataToCString(string szPath, string szTag, string szItem)
        {
            byte[] retVal = new byte[255];
            int count = GetPrivateProfileString(getBytes(szTag, "utf-8"), getBytes(szItem, "utf-8"), getBytes("", "utf-8"), retVal, 255, szPath);
            return Encoding.GetEncoding("utf-8").GetString(retVal, 0, count).Trim();

        }

        public static ushort GetIniDataToUshort(string szPath, string szTag, string szItem)
        {
            byte[] retVal = new byte[255];
            int i = GetPrivateProfileString(getBytes(szTag, "utf-8"), getBytes(szItem, "utf-8"), getBytes("", "utf-8"), retVal, 255, szPath);
            if (i == 0)
                return 0;
            else
                return ushort.Parse(Encoding.GetEncoding("utf-8").GetString(retVal, 0, i).Trim());
        }

        public static void WriteIniDataToCstring(string szPath, string szTag, string szItem, string szVal)
        {
            if (System.IO.File.Exists(szPath))
            {
                bool rtn;
                rtn = WritePrivateProfileString(getBytes(szTag, "utf-8"), getBytes(szItem, "utf-8"), getBytes(szVal, "utf-8"), szPath);
            }
            else
                // File is not exist!
                // MessageBox.Show(szPath + ";\r\n\r\n" + IMPQuickStart.Localization.System_Mesg[IMPQuickStart.Localization.Instance.language_index, 20]);
                ToolkitMessageBox.Show(szPath + ";\r\n\r\n" + "File is not exist!", "File is not exist", MessageBoxButton.OK, InfoType.Error,new ToolkitMessageBoxButtonText());
        }

        //lpString給null可以刪除整個Section，給空值會清除Section內所有Key
        public static void CSWritePrivateProfileSection(string szPath, string section, string lpString)
        {
            if (System.IO.File.Exists(szPath))
            {
                uint rtn;
                rtn = WritePrivateProfileSection(getBytes(section, "utf-8"), getBytes(lpString, "utf-8"), szPath);
            }
            else
                // File is not exist!
                // MessageBox.Show(szPath + ";\r\n\r\n" + IMPQuickStart.Localization.System_Mesg[IMPQuickStart.Localization.Instance.language_index, 20]);
                ToolkitMessageBox.Show(szPath + ";\r\n\r\n" + "File is not exist!", "File is not exist", MessageBoxButton.OK, InfoType.Error,new ToolkitMessageBoxButtonText());
        }
    }
    public class Class_InifileRW
    {
        public static String Current_ini_File_Path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Ini\\Modbus.ini");


        public static int Loadini()
        {
            if (!System.IO.Directory.Exists(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Ini")))
            {
                //新增資料夾
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Ini"));
            }

            if (!System.IO.File.Exists(Current_ini_File_Path))
            {
                string iniResult = "";
                iniResult = iniResult + "\r\n";
                iniResult = iniResult + "[Config]" + "\r\n";
                iniResult = iniResult + "Version=" + CNC_SNC_CSharp.ModbusInfo.Current_ini_Version + "\r\n";
                iniResult = iniResult + "Station=" + "1\r\n";
                iniResult = iniResult + "TCPport=" + "502\r\n";
                iniResult = iniResult + "COMNUM=" + CNC_SNC_CSharp.ModbusInfo.COM_Amount.ToString() + "\r\n";
                iniResult = iniResult + "TCPNUM=" + CNC_SNC_CSharp.ModbusInfo.TCP_Amount.ToString() + "\r\n";
                iniResult = iniResult + "\r\n";
                System.IO.File.WriteAllText(Current_ini_File_Path, iniResult);
            }
            return 0;
        }

        public static string GetIniDataToCString(String root, String key, String dft)
        {
            string retVal = Class_IniRW.GetIniDataToCString(Current_ini_File_Path, root, key);
            if (retVal.Length == 0) retVal = dft;
            return retVal;
        }

        public static Int32 ini_ReadInteger(String root, String key, Int32 DefVal)
        {
            Int32 res = 0;
            try
            {
                string retVal = Class_IniRW.GetIniDataToCString(Current_ini_File_Path, root, key);
                if (retVal.Length == 0) retVal = "0";
                res = Convert.ToInt32(retVal);
            }
            catch
            {
                res = DefVal;
            }
            return res;
        }
    }
}

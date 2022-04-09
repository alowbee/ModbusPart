using System.Runtime.InteropServices;
using System.Text;

namespace CNC_SNC_CSharp
{
    class ShareMem_main
    {
        public static bool ShareMemIsOpened = false;

        //WR MEMORY
        [DllImport("ShareMemDll.dll", EntryPoint = "ShareMem_Open")]
        public static extern int CS_ShareMem_Open();

        [DllImport("ShareMemDll.dll", EntryPoint = "ShareMem_Close")]
        public static extern void CS_ShareMem_Close();

        [DllImport("ShareMemDll.dll", EntryPoint = "Set_DX")]
        public static extern void CS_Set_DX(int node, int port, int bit, bool on);

        [DllImport("ShareMemDll.dll", EntryPoint = "Get_DX")]
        public static extern int CS_Get_DX(int node, int port, int bit);

        [DllImport("ShareMemDll.dll", EntryPoint = "Set_DY")]
        public static extern void CS_Set_DY(int node, int port, int bit, bool on);

        [DllImport("ShareMemDll.dll", EntryPoint = "Get_DY")]
        public static extern int CS_Get_DY(int node, int port, int bit);

        [DllImport("ShareMemDll.dll", EntryPoint = "Set_M")]
        public static extern void CS_Set_M(int pos, bool on);

        [DllImport("ShareMemDll.dll", EntryPoint = "Get_M")]
        public static extern int CS_Get_M(int pos);

        [DllImport("ShareMemDll.dll", EntryPoint = "Set_R")]
        public static extern void CS_Set_R(int pos, bool on);

        [DllImport("ShareMemDll.dll", EntryPoint = "Get_R")]
        public static extern int CS_Get_R(int pos);

        [DllImport("ShareMemDll.dll", EntryPoint = "Set_D")]
        public static extern void CS_Set_D(int pos, int val, bool IsDoubleWord);

        [DllImport("ShareMemDll.dll", EntryPoint = "Get_D")]
        public static extern int CS_Get_D(int pos, bool IsDoubleWord);

        [DllImport("ShareMemDll.dll", EntryPoint = "Set_D_String")]
        public static extern void CS_Set_D_String(int pos, string Input, int size);

        [DllImport("ShareMemDll.dll", EntryPoint = "Get_D_String")]
        public static extern void CS_Get_D_String(int pos, string Output, int size);

        [DllImport("ShareMemDll.dll", EntryPoint = "Set_W")]
        public static extern void CS_Set_W(int pos, int val, bool IsDoubleWord);

        [DllImport("ShareMemDll.dll", EntryPoint = "Get_W")]
        public static extern int CS_Get_W(int pos, bool IsDoubleWord);

        [DllImport("ShareMemDll.dll", EntryPoint = "Set_W_String")]
        public static extern void CS_Set_W_String(int pos, string Input, int size);

        [DllImport("ShareMemDll.dll", EntryPoint = "Get_W_String")]
        public static extern void CS_Get_W_String(int pos, StringBuilder Output, int size);
    }
}

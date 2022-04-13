using CNC_SNC_CSharp;
using ModbusInfoPart.Data;
using ModbusPart.Data;
using ModbusPart.Sub;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WPFToolkit.Controls;
using WPFToolkit.Data.Enum;

namespace ModbusPart.ViewModel
{
    public class DeviceViewModel : BindableBase
    {
        private bool isEnable;
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                isEnable = value;
                RaisePropertyChanged(nameof(IsEnable));
                SetEnable();
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
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                UCModbus.MainViewModel.CurrentNode.Name = value;
                UCModbus.MainViewModel.Pagetitle = value;
                RaisePropertyChanged(nameof(Name));
                SetName();
            }
        }

        private int error_reg;
        public int Error_reg
        {
            get { return error_reg; }
            set
            {
                error_reg = value;
                RaisePropertyChanged(nameof(Error_reg));
                SetError_reg();

            }
        }

        private int station;
        public int Station
        {
            get { return station; }
            set
            {
                station = value;
                RaisePropertyChanged(nameof(Station));
                SetStation();
            }
        }

        private int device_Read_Limitlen;
        public int Device_Read_Limitlen
        {
            get { return device_Read_Limitlen; }
            set
            {
                device_Read_Limitlen = value;
                RaisePropertyChanged(nameof(Device_Read_Limitlen));
                SetDevice_Read_Limitlen();

            }
        }

        private MapItem currentItem;
        public MapItem CurrentItem
        {
            get { return currentItem; }
            set
            {
                currentItem = value;
                RaisePropertyChanged(nameof(CurrentItem));
            }
        }

        private bool isReadMap = true;

        public bool IsReadMap
        {
            get { return isReadMap; }
            set
            {
                isReadMap = value;
                RaisePropertyChanged(nameof(CurrentItem));
            }
        }


        private ObservableCollection<MapItem> readMap;
        public ObservableCollection<MapItem> ReadMap
        {
            get { return readMap; }
            set
            {
                readMap = value;
                RaisePropertyChanged(nameof(ReadMap));


            }
        }

        private ObservableCollection<MapItem> writeMap;
        public ObservableCollection<MapItem> WriteMap
        {
            get { return writeMap; }
            set
            {
                writeMap = value;
                RaisePropertyChanged(nameof(WriteMap));

            }
        }

        public DelegateCommand SetEnableButton { get; set; }
        public DelegateCommand DeleteDeviceCommand { get; set; }
        public DelegateCommand<ObservableCollection<MapItem>> DeleteSelectedMapItemCommand { get; set; }
        public DelegateCommand SortMapItemCommand { get; set; }
        public DelegateCommand InsertMapItemCommand { get; set; }

        //enable
        private void SetEnable()
        {
            var currentnode = UCModbus.MainViewModel.CurrentNode;

            if (currentnode != null)
            {
                if (currentnode.NodeType == NodeType.TCPDevice)
                {
                    var portindex = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[portindex].EnableTCPDevice[deviceindex] = IsEnable;
                }
                else if (currentnode.NodeType == NodeType.SerialDevice)
                {
                    var portindex = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[portindex].EnableSerialDevice[deviceindex] = IsEnable;
                }
                UCModbus.FileSaveTrg = true;


            }
        }
        //name
        private void SetName()
        {
            if (UCModbus.MainViewModel.CurrentNode != null)
            {
                if (UCModbus.MainViewModel.CurrentNode.NodeType == NodeType.TCPDevice)
                {
                    var portindex = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[portindex].deviceName[deviceindex] = Name;
                }
                else if (UCModbus.MainViewModel.CurrentNode.NodeType == NodeType.SerialDevice)
                {
                    var portindex = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[portindex].deviceName[deviceindex] = Name;
                }
                UCModbus.MainViewModel.CurrentNode.Name = Name;
                UCModbus.MainViewModel.Pagetitle = Name;
                UCModbus.FileSaveTrg = true;


            }

        }
        // Error_reg
        private void SetError_reg()
        {
            if (UCModbus.MainViewModel.CurrentNode != null)
            {
                if (UCModbus.MainViewModel.CurrentNode.NodeType == NodeType.TCPDevice)
                {
                    var portindex = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[portindex].Error_reg[deviceindex] = Error_reg;
                }
                else if (UCModbus.MainViewModel.CurrentNode.NodeType == NodeType.SerialDevice)
                {
                    var portindex = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[portindex].Error_reg[deviceindex] = Error_reg;
                }
                UCModbus.FileSaveTrg = true;

            }

        }
        //station
        private void SetStation()
        {
            if (UCModbus.MainViewModel.CurrentNode != null)
            {
                if (UCModbus.MainViewModel.CurrentNode.NodeType == NodeType.TCPDevice)
                {
                    var portindex = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[portindex].Station[deviceindex] = Station;
                }
                else if (UCModbus.MainViewModel.CurrentNode.NodeType == NodeType.SerialDevice)
                {
                    var portindex = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[portindex].Station[deviceindex] = Station;
                }
                UCModbus.FileSaveTrg = true;

            }
        }
        //Device_Read_Limitlen
        private void SetDevice_Read_Limitlen()
        {
            if (UCModbus.MainViewModel.CurrentNode != null)
            {
                if (UCModbus.MainViewModel.CurrentNode.NodeType == NodeType.TCPDevice)
                {
                    var portindex = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.TCP[portindex].Device_Read_Limitlen[deviceindex] = Device_Read_Limitlen;
                }
                else if (UCModbus.MainViewModel.CurrentNode.NodeType == NodeType.SerialDevice)
                {
                    var portindex = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode.ParentNode);
                    var deviceindex = UCModbus.MainViewModel.CurrentNode.ParentNode.Children.IndexOf(UCModbus.MainViewModel.CurrentNode);
                    ModbusInfo.Serial[portindex].Device_Read_Limitlen[deviceindex] = Device_Read_Limitlen;
                }
                UCModbus.FileSaveTrg = true;

            }
        }

        //delete Device
        private void DeleteDevice()
        {
            var selectednode = UCModbus.MainViewModel.CurrentNode;
            if (selectednode.NodeType == NodeType.SerialDevice)
            {
                var deviceindex = selectednode.ParentNode.Children.IndexOf(selectednode);
                var portindex = UCModbus.MainViewModel.SerialMainNode.Children.IndexOf(selectednode.ParentNode);
                for (int j = deviceindex; j < ModbusInfo.nDeviceNUM - 1; j++)
                {
                    ModbusInfo.Serial[portindex].deviceName[j] = ModbusInfo.Serial[portindex].deviceName[j + 1];
                    ModbusInfo.Serial[portindex].EnableSerialDevice[j] = ModbusInfo.Serial[portindex].EnableSerialDevice[j + 1];
                    ModbusInfo.Serial[portindex].Station[j] = ModbusInfo.Serial[portindex].Station[j + 1];
                    ModbusInfo.Serial[portindex].Error_reg[j] = ModbusInfo.Serial[portindex].Error_reg[j + 1];
                    ModbusInfo.Serial[portindex].Device_Read_Limitlen[j] = ModbusInfo.Serial[portindex].Device_Read_Limitlen[j + 1];
                    ModbusInfo.Serial[portindex].strType[j] = ModbusInfo.Serial[portindex].strType[j + 1];

                    for (int k = 0; k < ModbusInfo.Serial[portindex].Modbus_w.GetLength(1); k++)
                        ModbusInfo.Serial[portindex].Modbus_w[j, k] = ModbusInfo.Serial[portindex].Modbus_w[j + 1, k];
                    for (int k = 0; k < ModbusInfo.Serial[portindex].Modbus_r.GetLength(1); k++)
                        ModbusInfo.Serial[portindex].Modbus_r[j, k] = ModbusInfo.Serial[portindex].Modbus_r[j + 1, k];
                }

                ModbusInfo.Serial[portindex].NUM--;
                selectednode.ParentNode.Children.Remove(selectednode);


            }
            else if (selectednode.NodeType == NodeType.TCPDevice)
            {
                var deviceindex = selectednode.ParentNode.Children.IndexOf(selectednode);
                var portindex = UCModbus.MainViewModel.TCPMainNode.Children.IndexOf(selectednode.ParentNode);
                for (int j = deviceindex; j < ModbusInfo.nDeviceNUM - 1; j++)
                {

                    ModbusInfo.TCP[portindex].deviceName[j] = ModbusInfo.TCP[portindex].deviceName[j + 1];
                    ModbusInfo.TCP[portindex].EnableTCPDevice[j] = ModbusInfo.TCP[portindex].EnableTCPDevice[j + 1];
                    ModbusInfo.TCP[portindex].Station[j] = ModbusInfo.TCP[portindex].Station[j + 1];
                    ModbusInfo.TCP[portindex].Error_reg[j] = ModbusInfo.TCP[portindex].Error_reg[j + 1];
                    ModbusInfo.TCP[portindex].Device_Read_Limitlen[j] = ModbusInfo.TCP[portindex].Device_Read_Limitlen[j + 1];
                    ModbusInfo.TCP[portindex].strType[j] = ModbusInfo.TCP[portindex].strType[j + 1];

                    for (int k = 0; k < ModbusInfo.TCP[portindex].Modbus_w.GetLength(1); k++)
                        ModbusInfo.TCP[portindex].Modbus_w[j, k] = ModbusInfo.TCP[portindex].Modbus_w[j + 1, k];
                    for (int k = 0; k < ModbusInfo.TCP[portindex].Modbus_r.GetLength(1); k++)
                        ModbusInfo.TCP[portindex].Modbus_r[j, k] = ModbusInfo.TCP[portindex].Modbus_r[j + 1, k];
                }

                ModbusInfo.TCP[portindex].NUM--;
                selectednode.ParentNode.Children.Remove(selectednode);



            }
            UCModbus.FileSaveTrg = true;

        }

        //deleta mapitem
        private void DeleteSelectedMapItem(ObservableCollection<MapItem> map)
        {
            if (map.Count < 2) return;
            var isdelete = ToolkitMessageBox.Show(Properties.Lang.Lang.Deletethisline, Properties.Lang.Lang.Warning, MessageBoxButton.YesNo, InfoType.Warning);
            if (isdelete == MessageBoxResult.Yes)
            {
                var SelectedRow = this.CurrentItem;
                if (SelectedRow != null)
                {
                    map.Remove(SelectedRow);
                }
                if (map.Count == 1 && map[0].IsComplete == true)
                {
                    map.Add(new MapItem());
                }
                for (int i = 0; i < map.Count; i++)
                {
                    map[i].Index = i + 1;
                }
                if (map == ReadMap)
                    DataHelper.SetReadMap();
                if (map == WriteMap)
                    DataHelper.SetWriteMap();
                UCModbus.FileSaveTrg = true;


            }

        }


        private void InsertSelectedMapItem(ObservableCollection<MapItem> map)
        {
            if (map.Count > 0)
            {
                var index = map.IndexOf(CurrentItem);
                map.Insert(index,new MapItem() {Index=index+1 });
                for (int i = index+1; i < map.Count; i++)
                {
                    map[i].Index = i+1;
                }

            }
            else
                return;

        }


        private void SortMapItem(ObservableCollection<MapItem> map)
        {
            if (map.Count > 0)
            {
                List<MapItem> sortedList = map.ToList();
                sortedList.Sort();
                map.Clear();
                for (int i = 0; i < sortedList.Count; i++)
                {
                    sortedList[i].Index = i + 1;
                    map.Add(sortedList[i]);
                }
            }
        }



        public DeviceViewModel()
        {

            ReadMap = new ObservableCollection<MapItem>();
            WriteMap = new ObservableCollection<MapItem>();
            this.SetEnableButton = new DelegateCommand(() =>
            {
                var window = new ModbusEN();
                if (CurrentItem.ButtonContent == Properties.Lang.Lang.Enable)

                    window.EnableComboBox.SelectedIndex = 1;
                else if (CurrentItem.ButtonContent == Properties.Lang.Lang.Disable)
                    window.EnableComboBox.SelectedIndex = 0;
                else
                {
                    window.EnableComboBox.SelectedIndex = 2;
                    var value = CurrentItem.ButtonContent.Remove(0, 1);
                    window.MValue.Value = Convert.ToInt32(value);
                }
                window.ShowDialog();
            });

            this.DeleteSelectedMapItemCommand = new DelegateCommand<ObservableCollection<MapItem>>(DeleteSelectedMapItem);
            this.SortMapItemCommand = new DelegateCommand(
                () =>
                {
                    if (isReadMap)
                        SortMapItem(ReadMap);
                    else
                        SortMapItem(WriteMap);

                    UCModbus.FileSaveTrg = true;
                });
            this.InsertMapItemCommand = new DelegateCommand(
            () =>
            {
                if (isReadMap)
                    InsertSelectedMapItem(ReadMap);
                else
                    InsertSelectedMapItem(WriteMap);

                UCModbus.FileSaveTrg = true;
            });

        }

    }




}

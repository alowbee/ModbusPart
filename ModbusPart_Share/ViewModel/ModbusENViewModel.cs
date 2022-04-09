using ModbusPart.Sub;
using Prism.Commands;
using Prism.Mvvm;

namespace ModbusPart.ViewModel
{


    public class ModbusENViewModel : BindableBase
    {

        private int selectedindex;
        public int SelectedIndex
        {
            get { return selectedindex; }
            set
            {
                if (selectedindex < 0 || selectedindex > 2)
                    return;
                selectedindex = value;
                RaisePropertyChanged(nameof(SelectedIndex));
            }
        }



        private int mValue;
        public int MValue
        {
            get { return mValue; }
            set
            {
                mValue = value;
                RaisePropertyChanged(nameof(MValue));
            }
        }

        public DelegateCommand OKCommand { get; set; }
        public DelegateCommand ExitCommand { get; set; }
        public ModbusENViewModel()
        {
            this.OKCommand = new DelegateCommand(() =>
            {
                if (UCModbus.MainViewModel.SubContent is UCDevice devicecontent)
                {
                    var item = devicecontent.viewModel.CurrentItem;
                    if (devicecontent.viewModel.CurrentItem != null)
                    {
                        if (SelectedIndex == 2)
                        { item.ButtonContent = "M" + MValue; }
                        else
                        {
                            //  item.ButtonContent = selecteditem.Content.ToString();
                        }
                    }
                }


                // this.Close();
            });
        }
    }
}

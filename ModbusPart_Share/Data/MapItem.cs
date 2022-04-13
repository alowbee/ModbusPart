using ModbusPart.Sub;
using Prism.Mvvm;
using System;
using System.ComponentModel;
using System.Linq;

namespace ModbusPart.Data
{
    public class MapItem : BindableBase, IComparable<MapItem>
    {
        private int index;
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                RaisePropertyChanged(nameof(Index));
            }
        }

        private int function;
        public int Function
        {
            get { return function; }
            set
            {
                function = value;
                RaisePropertyChanged(nameof(Function));
            }
        }

        private string tagAddress;
        public string TagAddress
        {
            get { return tagAddress; }
            set
            {

                tagAddress = value.PadLeft(4, '0');
                RaisePropertyChanged(nameof(TagAddress));
            }
        }

        private int? register;
        public int? Register
        {
            get { return register; }
            set
            {
                register = value;
                RaisePropertyChanged(nameof(Register));
            }
        }

        private int? length;
        public int? Length
        {
            get { return length; }
            set
            {
                length = value;
                RaisePropertyChanged(nameof(Length));
            }
        }

        private string comment;
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                RaisePropertyChanged(nameof(Comment));
            }
        }

        private string buttonContent;
        public string ButtonContent
        {
            get
            {
                return buttonContent;
            }
            set
            {
                buttonContent = value;
                RaisePropertyChanged(nameof(ButtonContent));
            }
        }

        private bool isComplete;
        public bool IsComplete
        {
            get
            {

                if (Function > -1 && TagAddress != null && Length != null && Register != null)
                    isComplete = true;
                else isComplete = false;

                return isComplete;
            }
            set
            {
                isComplete = value;
                RaisePropertyChanged(nameof(IsComplete));

            }
        }

        public MapItem()
        {
            this.Function = -1;
        }

        public int CompareTo(MapItem other)
        {

            if (this.Function < 0)
                return 1;
            else if (other.Function < 0)
                return -1;
            else if (this.Function < other.Function)
                return -1;
            else if (this.Function == other.Function)
            {
                if (this.Register == null)
                    return 1;
                else if (other.Register == null)
                    return -1;
                else if (this.Register < other.Register)
                    return -1;
                else
                    return 1;

            }
            else
                return 1;
        }
    }
}

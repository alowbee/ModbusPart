using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace ModbusPart.Data
{

    public enum NodeType
    {
        IMP, TCP, Serials, TCPNode, SerialNode, SerialDevice, TCPDevice
    }
    public class TreeViewNode : BindableBase
    {

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string toolTip;
        public string ToolTip
        {
            get { return toolTip; }
            set
            {
                toolTip = value;
                RaisePropertyChanged(nameof(ToolTip));
            }
        }

        private ObservableCollection<TreeViewNode> children;
        public ObservableCollection<TreeViewNode> Children
        {
            get { return children; }
            set
            {
                children = value;
                RaisePropertyChanged(nameof(Children));
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }

        private NodeType type;
        public NodeType NodeType
        {
            get { return type; }
            set
            {
                type = value;
                RaisePropertyChanged(nameof(NodeType));
            }
        }

        private TreeViewNode parentNode;
        public TreeViewNode ParentNode
        {
            get { return parentNode; }
            set
            {
                parentNode = value;
                RaisePropertyChanged(nameof(ParentNode));
            }
        }

        private bool isExpanded;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                isExpanded = value;
                RaisePropertyChanged(nameof(IsExpanded));
            }
        }




        public TreeViewNode()
        {

            IsSelected = false;
            Children = new ObservableCollection<TreeViewNode>();
        }

    }
}

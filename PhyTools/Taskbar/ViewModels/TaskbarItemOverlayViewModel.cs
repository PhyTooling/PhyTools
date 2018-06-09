using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Taskbar.ViewModels
{
    public class TaskbarItemOverlayViewModel : INotifyPropertyChanged
    {
        private int _count;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Count
        {
            get { return _count; }
            set
            {
                if (_count != value)
                {
                    _count = value;
                    var handler = PropertyChanged;
                    if (handler != null)
                    {
                        handler(this, new PropertyChangedEventArgs("Count"));
                    }
                }
            }
        }
    }
}
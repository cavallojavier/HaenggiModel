using System.ComponentModel;

namespace HaenggiModel.Presentation.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        //Extra Stuff, shows why a base ViewModel is useful 
        private bool? _CloseWindowFlag;

        //basic ViewModelBase 
        internal void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public bool? CloseWindowFlag
        {
            get { return _CloseWindowFlag; }
            set
            {
                _CloseWindowFlag = value;
                RaisePropertyChanged("CloseWindowFlag");
            }
        }

        //public virtual void CloseWindow(bool? result = true)
        //{
        //    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
        //    {
        //        CloseWindowFlag = CloseWindowFlag == null
        //            ? true
        //            : !CloseWindowFlag;
        //    }));
        //}
    }
}

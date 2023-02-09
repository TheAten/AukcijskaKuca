using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AukcijskaKuca.Helpers
{
    public class PropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //protected virtual void OnPropertyChanged(string propertyName)
        //{
        //    Application.Current.Dispatcher.BeginInvoke((Action)(() =>
        //    {
        //        PropertyChangedEventHandler handler = PropertyChanged;
        //        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        //    }));
        //}
    }
}

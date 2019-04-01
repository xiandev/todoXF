using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace ToDo.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged
    {

        #region " Events "

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region " Interfaces "

        public INavigation Navigation { get; set; }

        #endregion

        #region " Public Methods "

        public void OnPropertyChanged(params string[] propertyNames)
        {
            foreach(var propertyName in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }            
        }

        #endregion

    }
}

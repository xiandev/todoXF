using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ToDo.Models;
using Xamarin.Forms;

namespace ToDo.ViewModels
{
    public class TodoItemViewModel : ViewModel
    {

        #region " Variables & Properties "

        public TodoItem Item { get; private set; }
        public string StatusText => Item.Completed ? "Reactivate" : "Completed";

        #endregion

        #region " Events "

        public event EventHandler ItemStatusChanged;

        #endregion

        #region " Constructors "

        public TodoItemViewModel(TodoItem item)
        {
            Item = item;
        }

        #endregion

        #region " Commands "

        public ICommand ToggleCompleted => new Command((arg) =>
        {
            Item.Completed = !Item.Completed;
            ItemStatusChanged?.Invoke(this, new EventArgs());
        });

        #endregion

    }
}

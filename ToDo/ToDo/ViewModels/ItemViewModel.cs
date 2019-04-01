using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ToDo.Models;
using ToDo.Repositories;
using Xamarin.Forms;

namespace ToDo.ViewModels
{
    public class ItemViewModel : ViewModel
    {

        #region " Variables & Properties "

        private TodoItemRepository _repository;

        public TodoItem Item { get; set; }

        #endregion

        #region " Commands "

        public ICommand Save => new Command(async () =>
        {
            await _repository.AddOrUpdate(Item);
            await Navigation.PopAsync();
        });

        #endregion

        #region " Constructors "

        public ItemViewModel(TodoItemRepository repository)
        {
            this._repository = repository;
            Item = new TodoItem() { Due = DateTime.Now.AddDays(1) };
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDo.Models;
using ToDo.Repositories;
using ToDo.Views;
using Xamarin.Forms;

namespace ToDo.ViewModels
{
    public class MainViewModel : ViewModel
    {

        #region " Variables & Properties "

        private readonly TodoItemRepository _repository;
        public ObservableCollection<TodoItemViewModel> Items { get; set; }
        public TodoItemViewModel SelectedItem
        {
            get
            {
                return null;
            }
            set
            {
                Device.BeginInvokeOnMainThread(async () => await NavigateToItem(value));
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public bool ShowAll { get; set; }
        public string FilterText => ShowAll ? "All" : "Active";

        #endregion

        #region " Constructors "

        public MainViewModel(TodoItemRepository repository)
        {

            repository.OnItemAdded += (sender, item) => Items.Add(CreateTodoItemViewModel(item));
            repository.OnItemUpdated += (sender, item) => Task.Run(async () => await LoadData());

            this._repository = repository;
            Task.Run(async () =>
            {
                await LoadData();
            });
        }

        #endregion

        #region " Commands "

        public ICommand AddItem => new Command(async () =>
        {
            var itemView = Resolver.Resolve<ItemView>();
            await Navigation.PushAsync(itemView);
        });

        public ICommand ToggleFilter => new Command(async () =>
        {
            ShowAll = !ShowAll;
            await LoadData();
        });

        #endregion

        #region " Private Methods "

        private async Task LoadData()
        {
            var items = await _repository.GetItems();
            if (!ShowAll)
            {
                items = items.Where(x => x.Completed == false).ToList();
            }
            var itemViewModels = items.Select(i => CreateTodoItemViewModel(i));
            Items = new ObservableCollection<TodoItemViewModel>(itemViewModels);
        }

        private TodoItemViewModel CreateTodoItemViewModel(TodoItem item)
        {
            var itemViewModel = new TodoItemViewModel(item);
            itemViewModel.ItemStatusChanged += ItemViewModel_ItemStatusChanged;
            return itemViewModel;
        }

        private void ItemViewModel_ItemStatusChanged(object sender, EventArgs e)
        {
            if (sender is TodoItemViewModel item)
            {
                if (!ShowAll && item.Item.Completed)
                {
                    Items.Remove(item);
                }

                Task.Run(async () => await _repository.UpdateItem(item.Item));
            }
        }

        private async Task NavigateToItem(TodoItemViewModel item)
        {
            if(item == null)
            {
                return;
            }

            var itemView = Resolver.Resolve<ItemView>();
            var vm = itemView.BindingContext as ItemViewModel;
            vm.Item = item.Item;

            await Navigation.PushAsync(itemView);
        }

        #endregion

    }
}

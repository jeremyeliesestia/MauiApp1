using SQLite;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        int idSelected = -1;
        ObservableCollection<TodoItem> items = new ObservableCollection<TodoItem>();
        ObservableCollection<String> itemsName = new ObservableCollection<String>();
        TodoItemDatabase database = new TodoItemDatabase();
        //PrimaryKeyAttribute primaryKeyAttribute = new PrimaryKeyAttribute();

        public MainPage()
        {
            InitializeComponent();
            LoadItemsFromDatabase();

            //put Name of all ToDoItem in items to the StudentList

            StudentList.ItemsSource = itemsName;
        }

        private async void LoadItemsFromDatabase()
        {
            var todoItems = await database.GetItemsAsync();
            items.Clear();
            foreach (var item in todoItems)
            {
                items.Add(item);
                itemsName.Add(item.Name);
            }
        }

        private async void AddNewStudentInDatabase(object sender, EventArgs e)
        {

            if (StudentNameEntry.Text == "" || StudentNameEntry.Text == null)
                return;

            var todoItem = new TodoItem { Name = StudentNameEntry.Text };
            await database.SaveItemAsync(todoItem);
            items.Add(todoItem);
            itemsName.Add(todoItem.Name);




            // Afficher tous les éléments de la base de données dans la console
            var allItems = await database.GetItemsAsync();
            Debug.WriteLine("");
            foreach (var item in allItems)
            {
               Debug.WriteLine($"ID: {item.ID}, Name: {item.Name}, Done: {item.Done}");
            }
        }

        private async void RemoveStudentFromDatabase(object sender, EventArgs e)
        {
            if (StudentList.SelectedItem == null || idSelected == -1)
                return;

            //var pk = primaryKeyAttribute.Match(idSelected);

            await database.DeleteItemAsync(items[idSelected]);
            itemsName.RemoveAt(idSelected);
            idSelected = -1 ;
            StudentNameEntry.Text = "";

            var allItems = await database.GetItemsAsync();
            Debug.WriteLine("");
            foreach (var item in allItems)
            {
                Debug.WriteLine($"ID: {item.ID}, Name: {item.Name}, Done: {item.Done}");
            }


        }

        private void OnStudentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (StudentList.SelectedItem == null)
                return;

            string selectName = e.SelectedItem.ToString();
            StudentNameEntry.Text = selectName;
            idSelected = e.SelectedItemIndex;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
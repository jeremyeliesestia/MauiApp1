using MauiApp1.Class;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        ObservableCollection<Student> items = new ObservableCollection<Student>();
        ObservableCollection<String> itemsName = new ObservableCollection<String>();
        ObservableCollection<Matieres> Matieres = new ObservableCollection<Matieres>();
        ObservableCollection<SousMatieres> SousMatieres = new ObservableCollection<SousMatieres>();
        ObservableCollection<Competences> Competences = new ObservableCollection<Competences>();
        

        StudentDatabase database = new StudentDatabase();
        MatieresDatabase databaseMatieres = new MatieresDatabase();
        SousMatieresDatabase databaseSousMatieres = new SousMatieresDatabase();
        CompetencesDatabase databaseCompetences = new CompetencesDatabase();


        private int idSelected;

        //PrimaryKeyAttribute primaryKeyAttribute = new PrimaryKeyAttribute();

        public MainPage()
        {
            InitializeComponent();
            LoadItemsFromDatabase();
            LoadMatieresFromDatabase();
            LoadSousMatieresFromDatabase();
            LoadCompetencesFromDatabase();

            //put Name of all Student in items to the StudentList

            StudentList.ItemsSource = itemsName;
        }

        private async void LoadItemsFromDatabase()
        {
            var Students = await database.GetItemsAsync();
            items.Clear();
            foreach (var item in Students)
            {
                items.Add(item);
                itemsName.Add(item.Name);
            }
        }
        private async void LoadMatieresFromDatabase()
        {
            var matI = await databaseMatieres.GetItemsAsync();
            Matieres.Clear();
            foreach (var mat in matI)
            {
                Matieres.Add(mat);
            }
        }
        private async void LoadSousMatieresFromDatabase()
        {
            var sousmatI = await databaseSousMatieres.GetItemsAsync();
            SousMatieres.Clear();
            foreach (var sousmat in sousmatI)
            {
                SousMatieres.Add(sousmat);
            }
        }
        private async void LoadCompetencesFromDatabase()
        {
            var compI = await databaseCompetences.GetItemsAsync();
            Competences.Clear();
            foreach (var comp in compI)
            {
                Competences.Add(comp);
            }
        }

        private async void AddNewStudentInDatabase(object sender, EventArgs e)
        {

            if (StudentNameEntry.Text == "" || StudentNameEntry.Text == null)
                return;

            var Student = new Student { Name = StudentNameEntry.Text };
            await database.SaveItemAsync(Student);

            items.Add(Student);
            itemsName.Add(Student.Name);




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
            if (StudentList.SelectedItem == null)
                return;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == idSelected)
                {
                    await database.DeleteItemAsync(items[i]);
                    break;
                }
            }

            itemsName.Remove(StudentNameEntry.Text);
            
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

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name.ToString() == e.SelectedItem.ToString())
                {
                    StudentNameEntry.Text = items[i].Name.ToString();
                    idSelected = items[i].ID;
                    break;
                }
            }

        }

    }
}
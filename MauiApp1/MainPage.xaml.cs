
using System.Collections.ObjectModel;

namespace MauiApp1
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        ObservableCollection<string> items = new ObservableCollection<string>();

        public MainPage()
        {
            InitializeComponent();
            LoadItemsFromTextFile();
            StudentList.ItemsSource = items;
        }

        private void LoadItemsFromTextFile()
        {
            items.Clear();
            // Chemin relatif du répertoire de données de l'application
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //imprime dans la console le chemin du fichier
            string filePath = Path.Combine(folderPath, "Student.txt");

            // Lire les lignes du fichier et les ajouter à la liste items
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        string line;
                        while ((line = sr.ReadLine()) != null)
                        {
                            items.Add(line);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Le fichier Student.txt n'existe pas.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de la lecture du fichier : {ex.Message}");
            }
        }

        private void AddNewStudentInTextFile(object sender, EventArgs e)
        {
            // Chemin relatif du répertoire de données de l'application
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filePath = Path.Combine(folderPath, "Student.txt");

            // Ajouter le nouvel étudiant à la fin du fichier
            try
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(StudentNameEntry.Text);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de l'écriture du fichier : {ex.Message}");
            }

            // Mettre à jour la liste
            LoadItemsFromTextFile();
        }

        private void RemoveStudentFromTextFile(object sender, EventArgs e)
        {
            // Chemin relatif du répertoire de données de l'application
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string filePath = Path.Combine(folderPath, "Student.txt");

            // Récupérer le texte de StudentNameEntry.Text
            string studentName = StudentNameEntry.Text;

            // Supprimer la ligne correspondante dans le fichier
            try
            {
                if (File.Exists(filePath))
                {
                    List<string> lines = new List<string>(File.ReadAllLines(filePath));
                    lines.Remove(studentName);
                    File.WriteAllLines(filePath, lines);
                }
                else
                {
                    Console.WriteLine("Le fichier Student.txt n'existe pas.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors de la suppression de l'étudiant : {ex.Message}");
            }

            // Mettre à jour la liste
            LoadItemsFromTextFile();
        }

        private void OnStudentSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (StudentList.SelectedItem == null)
                return;

            // Mettre à jour le champ StudentNameEntry avec l'élément sélectionné
            StudentNameEntry.Text = e.SelectedItem.ToString();
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
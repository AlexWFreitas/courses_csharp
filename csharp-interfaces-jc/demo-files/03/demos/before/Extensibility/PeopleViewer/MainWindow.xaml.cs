using PersonRepository.CSV;
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.SQL;
using System.Windows;

namespace PeopleViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ServiceFetchButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateListBox(new ServiceRepository());
        }

        private void CSVFetchButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateListBox(new CSVRepository());
        }

        private void SQLFetchButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateListBox(new SQLRepository());
        }

        private void PopulateListBox(IPersonRepository repository)
        {
            ClearListBox();

            var people = repository.GetPeople();
            PersonListBox.ItemsSource = people;

            ShowRepositoryType(repository);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearListBox();
        }

        private void ClearListBox()
        {
            PersonListBox.ItemsSource = null;
            RepositoryTypeTextBlock.Text = string.Empty;
        }

        private void ShowRepositoryType(IPersonRepository repository)
        {
            RepositoryTypeTextBlock.Text = repository.GetType().ToString();
        }
    }
}

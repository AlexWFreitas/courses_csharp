using PersonRepository.CSV;
using PersonRepository.Interface;
using PersonRepository.Service;
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
            ClearListBox();

            IPersonRepository repository = new ServiceRepository();
            var people = repository.GetPeople();
            PersonListBox.ItemsSource = people;

            ShowRepositoryType(repository);
        }

        private void CSVFetchButton_Click(object sender, RoutedEventArgs e)
        {
            ClearListBox();

            IPersonRepository repository = new CSVRepository();
            var people = repository.GetPeople();
            PersonListBox.ItemsSource = people;

            ShowRepositoryType(repository);
        }

        private void SQLFetchButton_Click(object sender, RoutedEventArgs e)
        {

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

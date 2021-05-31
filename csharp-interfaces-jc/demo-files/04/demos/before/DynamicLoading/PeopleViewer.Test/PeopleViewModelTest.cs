using NUnit.Framework;
using System.Linq;

namespace PeopleViewer.Test
{
    public class PeopleViewModelTest
    {
        [Test]
        public void People_OnFetchData_IsPopulated()
        {
            // Arrange
            PeopleViewModel viewModel = new PeopleViewModel();
            int expected = 2;

            // Act
            viewModel.FetchData();
            int actual = viewModel.People.Count();

            // Assert
            Assert.IsNotNull(viewModel.People);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void People_OnClearData_IsEmpty()
        {
            // Arrange
            PeopleViewModel viewModel = new PeopleViewModel();
            viewModel.FetchData();
            int expected = 0;

            // Check Arrangement
            Assert.AreEqual(2, viewModel.People.Count(), "Invalid arrangement.");

            // Act
            viewModel.ClearData();
            int actual = viewModel.People.Count();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RepositoryType_OnCreation_ReturnsFakeRepositoryString()
        {
            // Arrange
            PeopleViewModel viewModel = new PeopleViewModel();
            string expected = "PersonRepository.Fake.FakeRepository";

            // Act
            string actual = viewModel.RepositoryType;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

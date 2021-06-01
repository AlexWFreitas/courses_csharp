using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleViewer.Presentation.Tests
{
    class FakeRepository : IPersonRepository
    {
        public IEnumerable<Person> GetPeople()
        {
            return TestData.testPeople;
        }

        public Person GetPerson(int id)
        {
            throw new NotImplementedException();
        }
    }
}

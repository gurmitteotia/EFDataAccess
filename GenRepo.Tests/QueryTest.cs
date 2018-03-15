using System.Linq;
using NUnit.Framework;

namespace GenRepo.Tests
{
    [TestFixture]
    public partial class QueryTest
    {

        [Test]
        public void Filter_items()
        {
            var items = new[] {new TestItem(10) {Salary = 5000}, new TestItem(12) {Salary = 1000},};
            var query = Query<TestItem>.Create(t => t.Salary > 4000);

            var filteredItems = query.Filter(items.AsQueryable());

            Assert.That(filteredItems , Is.EqualTo(new []{ new TestItem(10)}));
        }

        [Test]
        public void Filter_using_query_combined_with_and()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000 }, new TestItem(13){Salary = 6000}};
            var q1 = Query<TestItem>.Create(t => t.Salary > 4000);
            var q2 = Query<TestItem>.Create(t => t.Salary < 5500);

            var filteredItems = q1.And(q2).Filter(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(10) }));
        }

        [Test]
        public void Filter_using_query_combined_with_or()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000, Name  = "ram"}, new TestItem(13) { Salary = 6000 } };
            var q1 = Query<TestItem>.Create(t => t.Salary > 10000);
            var q2 = Query<TestItem>.Create(t => string.Equals(t.Name, "ram"));

            var filteredItems = q1.Or(q2).Filter(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(12) }));
        }

        [Test]
        public void Dynamic_filter_items()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000 }, };
            var query = Query<TestItem>.Create("Salary", OperationType.GreaterThan, 4000);

            var filteredItems = query.Filter(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(10) }));
        }

        [Test]
        public void Dynamic_filter_using_query_combined_with_and()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000 }, new TestItem(13) { Salary = 6000 } };
            var q1 = Query<TestItem>.Create("Salary", OperationType.GreaterThan, 4000);
            var q2 = Query<TestItem>.Create("Salary", OperationType.LessThan, 5500);
            
            var filteredItems = q1.And(q2).Filter(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(10) }));
        }

        [Test]
        public void Dynamic_filter_using_query_combined_with_or()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000, Name = "ram" }, new TestItem(13) { Salary = 6000 } };
            var q1 = Query<TestItem>.Create("Salary", OperationType.GreaterThan, 10000);
            var q2 = Query<TestItem>.Create("Name", OperationType.EqualTo, "ram");

            var filteredItems = q1.Or(q2).Filter(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(12) }));
        }

        [Test]
        public void Create_query_from_specification()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000 }, };
            var spec = new Specification<TestItem>(t => t.Salary > 4000);
            Query<TestItem> query = spec;
            var filteredItems = query.Filter(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(10) }));
        }

    }
}
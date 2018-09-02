using System.Linq;
using NUnit.Framework;

namespace GenRepo.Tests.Filter
{
    [TestFixture]
    public class FilterTest
    {

        [Test]
        public void Filter_items_on_known_field()
        {
            var items = new[] {new TestItem(10) {Salary = 5000}, new TestItem(12) {Salary = 1000},};
            var filter = Filter<TestItem>.Create(t => t.Salary > 4000);

            var filteredItems = filter.Apply(items.AsQueryable());

            Assert.That(filteredItems , Is.EqualTo(new []{ new TestItem(10)}));
        }

        [Test]
        public void Filters_combined_with_and()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000 }, new TestItem(13){Salary = 6000}};
            var f1 = Filter<TestItem>.Create(t => t.Salary > 4000);
            var f2 = Filter<TestItem>.Create(t => t.Salary < 5500);

            var filteredItems = f1.And(f2).Apply(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(10) }));
        }

        [Test]
        public void Filters_combined_with_or()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000, Name  = "ram"}, new TestItem(13) { Salary = 6000 } };
            var f1 = Filter<TestItem>.Create(t => t.Salary > 10000);
            var f2 = Filter<TestItem>.Create(t => string.Equals(t.Name, "ram"));

            var filteredItems = f1.Or(f2).Apply(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(12) }));
        }

        [Test]
        public void Filter_using_dynamic_field()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000 }, };
            var filter = Filter<TestItem>.Create("Salary", OperationType.GreaterThan, 4000);

            var filteredItems = filter.Apply(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(10) }));
        }

        [Test]
        public void Dynamic_filters_combined_with_and()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000 }, new TestItem(13) { Salary = 6000 } };
            var f1 = Filter<TestItem>.Create("Salary", OperationType.GreaterThan, 4000);
            var f2 = Filter<TestItem>.Create("Salary", OperationType.LessThan, 5500);
            
            var filteredItems = f1.And(f2).Apply(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(10) }));
        }

        [Test]
        public void Dynamic_filters_combined_with_or()
        {
            var items = new[] { new TestItem(10) { Salary = 5000 }, new TestItem(12) { Salary = 1000, Name = "ram" }, new TestItem(13) { Salary = 6000 } };
            var f1 = Filter<TestItem>.Create("Salary", OperationType.GreaterThan, 10000);
            var f2 = Filter<TestItem>.Create("Name", OperationType.EqualTo, "ram");

            var filteredItems = f1.Or(f2).Apply(items.AsQueryable());

            Assert.That(filteredItems, Is.EqualTo(new[] { new TestItem(12) }));
        }
    }
}
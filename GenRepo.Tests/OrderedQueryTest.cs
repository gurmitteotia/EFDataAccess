using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GenRepo.Tests
{
    [TestFixture]
    public class OrderedQueryTest
    {
        private IQueryable<TestItem> _testItems;

        [SetUp]
        public void Setup()
        {
            _testItems = new List<TestItem>()
            {
                new TestItem(10){Name = "Ram", Salary = 10000},
                new TestItem(11){Name = "David", Salary = 20000},
                new TestItem(12){Name = "Shyam", Salary = 15000},
                new TestItem(13){Name = "Amit", Salary = 1000},
            }.AsQueryable();
        }

        [Test]
        public void Order_the_items_in_ascending_order_with_fix_key()
        {
            var filter = Filter<TestItem>.Create(t => t.Salary > 1500);
            var sortQuery = filter.Query.OrderBy(o => o.Asc(t => t.Salary));

            var sortedItems = sortQuery.Execute(_testItems).ToList();

            Assert.That(sortedItems, Is.EqualTo(new[]{new TestItem(10), new TestItem(12), new TestItem(11)}));
        }

        [Test]
        public void Order_the_items_in_descending_order_with_fix_key()
        {
            var filter = Filter<TestItem>.Create(t => t.Salary > 1500);
            var sortQuery = filter.Query.OrderBy(o => o.Desc(t => t.Salary));

            var sortedItems = sortQuery.Execute(_testItems).ToList();

            Assert.That(sortedItems, Is.EqualTo(new[] { new TestItem(11), new TestItem(12), new TestItem(10) }));
        }

        [Test]
        public void Order_the_items_in_ascending_order_with_dynamic_key()
        {
            var filter = Filter<TestItem>.Create(t => t.Salary > 1500);
            var sortQuery = filter.Query.OrderBy(o => o.Asc("Salary"));

            var sortedItems = sortQuery.Execute(_testItems).ToList();

            Assert.That(sortedItems, Is.EqualTo(new[] { new TestItem(10), new TestItem(12), new TestItem(11) }));
        }

        [Test]
        public void Order_the_items_in_descending_order_with_dynamic_key()
        {
            var filter = Filter<TestItem>.Create(t => t.Salary > 1500);
            var sortQuery = filter.Query.OrderBy(o => o.Desc("Salary"));

            var sortedItems = sortQuery.Execute(_testItems).ToList();

            Assert.That(sortedItems, Is.EqualTo(new[] { new TestItem(11), new TestItem(12), new TestItem(10) }));
        }
    }
}
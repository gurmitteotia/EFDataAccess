using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using NUnit.Framework;

namespace GenRepo.Tests
{
    [TestFixture]
    public class DynamicFilterTest
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
        public void Filter_equalto()
        {
            var filter = Filter<TestItem>.Create("Name", OperationType.EqualTo, "Ram");
            Assert.That(filter.Apply(_testItems).Count(), Is.EqualTo(1));
        }

        [Test]
        public void Filter_notequalto()
        {
            var filter = Filter<TestItem>.Create("Name", OperationType.NotEqualTo, "Ram");
            Assert.That(filter.Apply(_testItems).Count(), Is.EqualTo(3));
        }

        [Test]
        public void Filter_greaterthan()
        {
            var filter = Filter<TestItem>.Create("Salary", OperationType.GreaterThan, "10000");
            Assert.That(filter.Apply(_testItems).Count(), Is.EqualTo(2));
        }

        [Test]
        public void Filter_greaterthan_or_equal()
        {
            var filter = Filter<TestItem>.Create("Salary", OperationType.GreaterThanEqualTo, "10000");
            Assert.That(filter.Apply(_testItems).Count(), Is.EqualTo(3));
        }

        [Test]
        public void Filter_lessthan()
        {
            var filter = Filter<TestItem>.Create("Salary", OperationType.LessThan, "10000");
            Assert.That(filter.Apply(_testItems).Count(), Is.EqualTo(1));
        }

        [Test]
        public void Filter_lessthan_or_equal()
        {
            var filter = Filter<TestItem>.Create("Salary", OperationType.LessThanEqualTo, "10000");
            Assert.That(filter.Apply(_testItems).Count(), Is.EqualTo(2));
        }

        [Test]
        public void Filter_startswith()
        {
            var filter = Filter<TestItem>.Create("Name", OperationType.StartsWith, "Da");
            Assert.That(filter.Apply(_testItems).Count(), Is.EqualTo(1));
        }

        [Test]
        public void Filter_ends_with()
        {
            var filter = Filter<TestItem>.Create("Name", OperationType.EndsWith, "am");
            Assert.That(filter.Apply(_testItems).Count(), Is.EqualTo(2));
        }

        [Test]
        public void Filter_contains()
        {
            var filter = Filter<TestItem>.Create("Name", OperationType.Contains, "am");
            Assert.That(filter.Apply(_testItems).Count(), Is.EqualTo(2));
        }

        [Test]
        public void Throws_exception_for_invalid_operator_for_a_numeric_type()
        {
            Assert.Throws<ArgumentException>(() => Filter<TestItem>.Create("Salary", OperationType.Contains, "10"));
            Assert.Throws<ArgumentException>(() => Filter<TestItem>.Create("Salary", OperationType.EndsWith, "10"));
            Assert.Throws<ArgumentException>(() => Filter<TestItem>.Create("Salary", OperationType.StartsWith, "10"));
        }
    }
}
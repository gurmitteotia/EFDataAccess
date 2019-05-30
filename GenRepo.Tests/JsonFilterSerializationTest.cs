using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GenRepo.Tests
{
    [TestFixture]
    public class JsonFilterSerializationTest
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
        public void Serialize_and_filter_with_one_filter_expression()
        {
            var expression = new LeafFilterExpression()
                {Operation = OperationType.GreaterThan, Property = "Salary", Value = 10000};
            var json = new JsonFilterExpression(expression.Json());

            var filteredItems = json.Filter<TestItem>().Apply(_testItems);

            Assert.That(filteredItems.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Serialize_and_filter_with_combined_and_filter_expression()
        {
            var lhs = new LeafFilterExpression()
                { Operation = OperationType.GreaterThan, Property = "Salary", Value = 10000 };

            var rhs = new LeafFilterExpression()
                { Operation = OperationType.Contains, Property = "Name", Value = "am" };

            var combined = new CompositeFilterExpression()
                {LHS = lhs, RHS = rhs, LogicalOperator = LogicalOperator.And};

            var json = new JsonFilterExpression(combined.Json());

            var filteredItems = json.Filter<TestItem>().Apply(_testItems);

            Assert.That(filteredItems.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Serialize_and_filter_with_combined_or_filter_expression()
        {
            var lhs = new LeafFilterExpression()
                { Operation = OperationType.GreaterThan, Property = "Salary", Value = 10000 };

            var rhs = new LeafFilterExpression()
                { Operation = OperationType.Contains, Property = "Name", Value = "am" };

            var combined = new CompositeFilterExpression()
                { LHS = lhs, RHS = rhs, LogicalOperator = LogicalOperator.Or };

            var json = new JsonFilterExpression(combined.Json());

            var filteredItems = json.Filter<TestItem>().Apply(_testItems);

            Assert.That(filteredItems.Count(), Is.EqualTo(3));
        }
    }
}
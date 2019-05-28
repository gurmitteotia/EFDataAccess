// /Copyright (c) Gurmit Teotia. Please see the LICENSE file in the project root folder for license information.

using System;
using System.Linq;
using NUnit.Framework;

namespace GenRepo.Tests
{
    [TestFixture]
    public class QueryProjectionTest
    {

        [Test]
        public void Project_query_to_specific_field_using_expression()
        {
            var items = new[] { new TestItem(10) { Name = "Ram", Salary = 6000 }, new TestItem(12) { Name = "Shyam", Salary = 1000 }, };
            Query<TestItem> query = Filter<TestItem>.Create(t => t.Salary > 5000).Query;
            var projection = query.Projection(e => new {e.Name, e.Salary});

            var filteredItems = projection.Evaluate(items.AsQueryable()).ToArray();

            Assert.That(filteredItems.Length, Is.EqualTo(1));
            Assert.That(filteredItems[0].Name, Is.EqualTo("Ram"));
        }
    }
}
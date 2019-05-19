// /Copyright (c) Gurmit Teotia. Please see the LICENSE file in the project root folder for license information.

using NUnit.Framework;

namespace GenRepo.Tests
{
    [TestFixture]
    public class QueryProjectionTest
    {

        [Test]
        public void Project_query_to_specific_field_using_expression()
        {
            var items = new[] { new TestItem(10) { Name = "Ram", Salary = 5000 }, new TestItem(12) { Name = "Shyam", Salary = 1000 }, };
            var query = Filter<TestItem>.Create(t => t.Salary > 5000);
        }
    }
}
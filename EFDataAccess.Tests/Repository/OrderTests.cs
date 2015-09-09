using System.Collections.Generic;
using System.Linq;
using EFDataAccess.Repository;
using NUnit.Framework;

namespace EFDataAccess.Tests.Repository
{
    [TestFixture]
    public class OrderTests
    {
        private IQueryable<TestClass> _unorderedObjects;
 
        private Order<TestClass> _order;

        [SetUp]
        public void Setup()
        {
            var testObjects = new List<TestClass>();
            testObjects.Add(new TestClass(){Name = "David", Salary = 4000});
            testObjects.Add(new TestClass() { Name = "Aby", Salary = 6000 });
            testObjects.Add(new TestClass() { Name = "Elan", Salary = 5000 });

            _unorderedObjects = testObjects.AsQueryable();
            _order = new Order<TestClass>();
        }

        [Test]
        public void Objects_can_be_sorted_in_ascending_order_by_known_key()
        {
            var orderBy = _order.InAscOrderOf(o => o.Salary);
            
            var sortedObjects = orderBy.ApplyOrder(_unorderedObjects);

            VerifyThatObjectsAreSortedInAscendingOrderOfSalary(sortedObjects);
        }

        [Test]
        public void Objects_can_be_sorted_in_decending_order_by_known_key()
        {
            var orderBy = _order.InDescOrderOf(o => o.Salary);

            var sortedObjects = orderBy.ApplyOrder(_unorderedObjects);

            VerifyThatObjectsAreSortedInDecendingOrderOfSalary(sortedObjects);
        }

        [Test]
        public void Objects_can_be_sorted_in_ascending_order_by_dynamic_key()
        {
            var orderBy = _order.InAscOrderOf("Salary");

            var sortedObjects = orderBy.ApplyOrder(_unorderedObjects);

            VerifyThatObjectsAreSortedInAscendingOrderOfSalary(sortedObjects);
        }

        [Test]
        public void Objects_can_be_sorted_in_decending_order_by_dynamic_key()
        {
            var orderBy = _order.InDescOrderOf("Salary");

            var sortedObjects = orderBy.ApplyOrder(_unorderedObjects);

            VerifyThatObjectsAreSortedInDecendingOrderOfSalary(sortedObjects);
        }

        [Test]
        public void Objects_can_be_sorted_in_parameterized_ascending_order_by_known_key()
        {
            var orderBy = _order.InOrderOf(o => o.Salary,OrderType.Ascending);

            var sortedObjects = orderBy.ApplyOrder(_unorderedObjects);

            VerifyThatObjectsAreSortedInAscendingOrderOfSalary(sortedObjects);
        }

        [Test]
        public void Objects_can_be_sorted_in_parameterized_decending_order_by_known_key()
        {
            var orderBy = _order.InOrderOf(o => o.Salary, OrderType.Descending);


            var sortedObjects = orderBy.ApplyOrder(_unorderedObjects);

            VerifyThatObjectsAreSortedInDecendingOrderOfSalary(sortedObjects);
        }

        [Test]
        public void Objects_can_be_sorted_in_parameterized_ascending_order_by_dynamic_key()
        {
            var orderBy = _order.InOrderOf("Salary", OrderType.Ascending);

            var sortedObjects = orderBy.ApplyOrder(_unorderedObjects);

            VerifyThatObjectsAreSortedInAscendingOrderOfSalary(sortedObjects);
        }

        [Test]
        public void Objects_can_be_sorted_in_parameterized_decending_order_by_dynamic_key()
        {
            var orderBy = _order.InOrderOf("Salary", OrderType.Descending);


            var sortedObjects = orderBy.ApplyOrder(_unorderedObjects);

            VerifyThatObjectsAreSortedInDecendingOrderOfSalary(sortedObjects);
        }


        private void VerifyThatObjectsAreSortedInAscendingOrderOfSalary(IEnumerable<TestClass> testObjects)
        {
            var testObjectList = testObjects.ToList();

            Assert.AreEqual("David", testObjectList[0].Name);
            Assert.AreEqual(4000, testObjectList[0].Salary);

            Assert.AreEqual("Elan", testObjectList[1].Name);
            Assert.AreEqual(5000, testObjectList[1].Salary);

            Assert.AreEqual("Aby", testObjectList[2].Name);
            Assert.AreEqual(6000, testObjectList[2].Salary);
        }

        private void VerifyThatObjectsAreSortedInDecendingOrderOfSalary(IEnumerable<TestClass> testObjects)
        {
            var testObjectList = testObjects.ToList();

            Assert.AreEqual("Aby", testObjectList[0].Name);
            Assert.AreEqual(6000, testObjectList[0].Salary);
           
            Assert.AreEqual("Elan", testObjectList[1].Name);
            Assert.AreEqual(5000, testObjectList[1].Salary);

            Assert.AreEqual("David", testObjectList[2].Name);
            Assert.AreEqual(4000, testObjectList[2].Salary);
        }

        private class TestClass
        {
            public string Name { get; set; }

            public int Salary { get; set; }  
        }
    }
}
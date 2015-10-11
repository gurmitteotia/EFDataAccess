using System;
using EFDataAccess.Spec;
using NUnit.Framework;

namespace EFDataAccess.Tests.Spec
{
    [TestFixture]
    public class DynamicSpecificationTests
    {
        private TestClass _candidateObject;

        [SetUp]
        public void Setup()
        {
            _candidateObject = new TestClass() { Salary = 5000 };
        }

        [Test]
        public void EqualTo_test()
        {
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.EqualTo, 5000).IsSatisfiedBy(_candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.EqualTo, 4000).IsSatisfiedBy(_candidateObject), Is.False);
        }

        [Test]
        public void NotEqualTo_test()
        {
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.NotEqualTo, 4000).IsSatisfiedBy(_candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.NotEqualTo, 5000).IsSatisfiedBy(_candidateObject), Is.False);
        }

        [Test]
        public void GreaterThan_test()
        {
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.GreaterThan, 4000).IsSatisfiedBy(_candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.GreaterThan, 6000).IsSatisfiedBy(_candidateObject), Is.False);
        }

        [Test]
        public void LessThan_test()
        {
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.LessThan, 6000).IsSatisfiedBy(_candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.LessThan, 4000).IsSatisfiedBy(_candidateObject), Is.False);
        }


        [Test]
        public void GreaterThanEqualTo_test()
        {
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.GreaterThanEqualTo, 5000).IsSatisfiedBy(_candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.GreaterThanEqualTo, 4000).IsSatisfiedBy(_candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.GreaterThanEqualTo, 6000).IsSatisfiedBy(_candidateObject), Is.False);
        }

        [Test]
        public void LessThanEqualTo_test()
        {
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.LessThanEqualTo, 6000).IsSatisfiedBy(_candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.LessThanEqualTo, 5000).IsSatisfiedBy(_candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Salary", OperationType.LessThanEqualTo, 3000).IsSatisfiedBy(_candidateObject), Is.False);
        }

        [Test]
        public void Contains_test()
        {
            var candidateObject = new TestClass() { Department = "Sales & Marketing" };

            Assert.That(new DynamicSpecification<TestClass>("Department", OperationType.Contains, "Market").IsSatisfiedBy(candidateObject),Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Department", OperationType.Contains, "HR").IsSatisfiedBy(candidateObject), Is.False);
        }

        [Test]
        public void StartWith_test()
        {
            var candidateObject = new TestClass() { Department = "Sales & Marketing" };

            Assert.That(new DynamicSpecification<TestClass>("Department", OperationType.StartsWith, "Sales").IsSatisfiedBy(candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Department", OperationType.StartsWith, "HR").IsSatisfiedBy(candidateObject), Is.False);
        }

        [Test]
        public void EndsWith_test()
        {
            var candidateObject = new TestClass() { Department = "Sales & Marketing" };

            Assert.That(new DynamicSpecification<TestClass>("Department", OperationType.EndsWith, "Marketing").IsSatisfiedBy(candidateObject), Is.True);
            Assert.That(new DynamicSpecification<TestClass>("Department", OperationType.EndsWith, "HR").IsSatisfiedBy(candidateObject), Is.False);
        }

        [Test]
        public void Throws_exception_when_dynamic_property_is_not_found_in_field_and_properties_of_candidate_object()
        {
            var specification = new DynamicSpecification<TestClass>("Age", OperationType.GreaterThan, 30);

            TestDelegate matchingOfSpecification = ()=> specification.IsSatisfiedBy(_candidateObject);

            Assert.That(matchingOfSpecification,Throws.TypeOf<ArgumentException>());

        }

        private class TestClass
        {
            internal int Salary;

            public string Department { get; set; }
        }
    }
}
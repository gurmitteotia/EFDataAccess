using System;
using EFDataAccess.Spec;
using NUnit.Framework;

namespace EFDataAccess.Tests.Spec
{
    [TestFixture]
    public class DynamicSpecificationTests
    {
        [Test]
        public void Specification_with_equal_to_operation_type_is_satisfied_when_candiate_property_is_equal_to_dynamic_value()
        {
            var candidateObject = new TestClass() {Salary = 5000};
            var specification = new DynamicSpecification<TestClass>("Salary",OperationType.EqualTo, 5000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(satisfied);
        }

        [Test]
        public void Specification_with_equal_to_operation_type_is_not_satisfied_when_candiate_property_is_not_equal_to_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 5000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.EqualTo, 4000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(satisfied);
        }

        [Test]
        public void Specification_with_not_equal_to_operation_type_is_satisfied_when_candiate_property_is_not_equal_to_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 5000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.NotEqualTo, 4000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(satisfied);
        }

        [Test]
        public void Specification_with_not_equal_to_operation_type_is_not_satisfied_when_candiate_property_is_equal_to_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 5000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.NotEqualTo, 5000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(satisfied);
        }

        [Test]
        public void Specification_with_greater_then_operation_type_is_satisfied_when_candiate_property_is_greater_than_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 5000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.GreaterThan, 4000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(satisfied);
        }

        [Test]
        public void Specification_with_greater_then_operation_type_is_not_satisfied_when_candiate_property_is_less_than_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 4000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.GreaterThan, 5000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(satisfied);
        }

        [Test]
        public void Specification_with_greater_then_equal_to_operation_type_is_satisfied_when_candiate_property_is_greater_than_or_equal_to_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 4000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.GreaterThanEqualTo, 4000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(satisfied);
        }

        [Test]
        public void Specification_with_greater_then_equal_to_operation_type_is_not_satisfied_when_candiate_property_is_less_than_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 4000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.GreaterThanEqualTo, 5000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(satisfied);
        }

        [Test]
        public void Specification_with_less_than_operation_type_is_satisfied_when_candiate_property_is_less_than_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 4000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.LessThan, 5000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(satisfied);
        }

        [Test]
        public void Specification_with_less_than_operation_type_is_not_satisfied_when_candiate_property_is_greater_than_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 6000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.LessThan, 5000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(satisfied);
        }

        [Test]
        public void Specification_with_less_than_equal_to_operation_type_is_satisfied_when_candiate_property_is_less_than_or_equal_to_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 4000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.LessThanEqualTo, 4000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(satisfied);
        }

        [Test]
        public void Specification_with_less_than_equal_to_operation_type_is_not_satisfied_when_candiate_property_is_greater_than_dynamic_value()
        {
            var candidateObject = new TestClass() { Salary = 6000 };
            var specification = new DynamicSpecification<TestClass>("Salary", OperationType.LessThanEqualTo, 5000);

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(satisfied);
        }

        [Test]
        public void Specification_with_contains_operation_type_is_satisfied_when_candiate_property_has_dynamic_value()
        {
            var candidateObject = new TestClass() { Department = "Sales & Marketing" };
            var specification = new DynamicSpecification<TestClass>("Department", OperationType.Contains, "Market");

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(satisfied);
        }

        [Test]
        public void Specification_with_contains_operation_type_is_not_satisfied_when_candiate_property_does_not_has_dynamic_value()
        {
            var candidateObject = new TestClass() { Department = "Sales & Marketing" };
            var specification = new DynamicSpecification<TestClass>("Department", OperationType.Contains, "HR");

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.False(satisfied);
        }

        [Test]
        public void Specification_with_start_with_operation_type_is_satisfied_when_candiate_property_begins_with_dynamic_value()
        {
            var candidateObject = new TestClass() { Department = "Sales & Marketing" };
            var specification = new DynamicSpecification<TestClass>("Department", OperationType.StartsWith, "Sales");

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(satisfied);
        }

        [Test]
        public void Specification_with_start_with_operation_type_is_not_satisfied_when_candiate_property_does_not_begin_with_dynamic_value()
        {
            var candidateObject = new TestClass() { Department = "Sales & Marketing" };
            var specification = new DynamicSpecification<TestClass>("Department", OperationType.Contains, "HR");

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.False(satisfied);
        }

        [Test]
        public void Specification_with_ends_with_operation_type_is_satisfied_when_candiate_property_end_with_dynamic_value()
        {
            var candidateObject = new TestClass() { Department = "Sales & Marketing" };
            var specification = new DynamicSpecification<TestClass>("Department", OperationType.EndsWith, "Marketing");

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(satisfied);
        }

        [Test]
        public void Specification_with_ends_with_operation_type_is_not_satisfied_when_candiate_property_does_not_end_with_dynamic_value()
        {
            var candidateObject = new TestClass() { Department = "Sales & Marketing" };
            var specification = new DynamicSpecification<TestClass>("Department", OperationType.EndsWith, "HR");

            var satisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.False(satisfied);
        }

        [Test]
        public void Throws_exception_when_dynamic_property_is_not_found_in_field_and_properties_of_candidate_object()
        {
            var candidateObject = new TestClass();
            var specification = new DynamicSpecification<TestClass>("Name", OperationType.EndsWith, "Smith");

            TestDelegate matchingOfSpecification = ()=> specification.IsSatisfiedBy(candidateObject);

            Assert.That(matchingOfSpecification,Throws.TypeOf<ArgumentException>());

        }

        private class TestClass
        {
            internal int Salary;

            public string Department { get; set; }
        }
    }
}
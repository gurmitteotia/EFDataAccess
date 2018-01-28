using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GeePo.Tests.Spec
{
    [TestFixture]
    public class GenericSpecificationTests
    {
        [Test]
        public void Specification_is_satisfied_when_expression_is_true_for_candidate_object()
        {
            var candidateObject = new TestClass() {Salary = 5000};
            var specification = new GenericSpecification<TestClass>(t=>t.Salary>4000);

            var isSatisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(isSatisfied);
        }

        [Test]
        public void Specification_is_not_satisfied_when_expressions_is_false_for_candidate_object()
        {
            var candidateObject = new TestClass() { Salary = 3000 };
            var specification = new GenericSpecification<TestClass>(t => t.Salary > 4000);

            var isSatisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(isSatisfied);
        }

        [Test]
        public void And_specification_is_satisfied_when_both_expressions_are_true_for_candidate_obect()
        {
            var candidateObject = new TestClass() { DepartmentName = "Sales", Salary = 5000 };
           
            var salarySpecification = new GenericSpecification<TestClass>(t => t.Salary > 4000);
            var departmentSpecification = new GenericSpecification<TestClass>(t=>t.DepartmentName=="Sales");
            var andSpecification = salarySpecification.And(departmentSpecification);

            var isSatisfied = andSpecification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(isSatisfied);
        }

        [Test]
        public void And_specification_is_not_satisfied_when_either_of_expression_is_false_for_candidate_obect()
        {
            var candidateObject = new TestClass() { DepartmentName = "Sales", Salary = 3000 };

            var salarySpecification = new GenericSpecification<TestClass>(t => t.Salary > 4000);
            var departmentSpecification = new GenericSpecification<TestClass>(t => t.DepartmentName == "Sales");
            var andSpecification = salarySpecification.And(departmentSpecification);

            var isSatisfied = andSpecification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(isSatisfied);
        }

        [Test]
        public void Or_specification_is_satisfied_when_either_of_expression_is_true_for_candidate_obect()
        {
            var candidateObject = new TestClass() { DepartmentName = "Sales", Salary = 3000 };

            var salarySpecification = new GenericSpecification<TestClass>(t => t.Salary > 4000);
            var departmentSpecification = new GenericSpecification<TestClass>(t => t.DepartmentName == "Sales");
            var orSpecification = salarySpecification.Or(departmentSpecification);

            var isSatisfied = orSpecification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(isSatisfied);
        }

        [Test]
        public void Or_specification_is_not_satisfied_when_both_expressions_are_false_for_candidate_obect()
        {
            var candidateObject = new TestClass() { DepartmentName = "Sales", Salary = 3000 };

            var salarySpecification = new GenericSpecification<TestClass>(t => t.Salary > 4000);
            var departmentSpecification = new GenericSpecification<TestClass>(t => t.DepartmentName == "HR");
            var orSpecification = salarySpecification.Or(departmentSpecification);

            var isSatisfied = orSpecification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(isSatisfied);
        }

        [Test]
        public void Specification_filters_out_non_matching_objects_from_iqueryable()
        {
            var testObjects = new List<TestClass>();
            testObjects.Add(new TestClass(){ Salary = 3000});
            testObjects.Add(new TestClass() { Salary = 5000 });
            testObjects.Add(new TestClass() { Salary = 6000 });

            var specification = new GenericSpecification<TestClass>(t => t.Salary > 4000);

            var filteredQuery = specification.Filter(testObjects.AsQueryable());

            AsserThatAllTestObjectHasSalaryGreaterThan(filteredQuery, 4000);
        }

        [Test]
        public void Specification_filters_out_non_matching_objects_from_ienumerable()
        {
            var testObjects = new List<TestClass>();
            testObjects.Add(new TestClass() { Salary = 3000 });
            testObjects.Add(new TestClass() { Salary = 5000 });
            testObjects.Add(new TestClass() { Salary = 6000 });

            var specification = new GenericSpecification<TestClass>(t => t.Salary > 4000);

            var filteredQuery = specification.Filter(testObjects);

            AsserThatAllTestObjectHasSalaryGreaterThan(filteredQuery, 4000);
        }

        private void AsserThatAllTestObjectHasSalaryGreaterThan(IEnumerable<TestClass> query, int salary)
        {
            foreach (var testClass in query)
            {
                Assert.That(testClass.Salary, Is.GreaterThan(salary));
            }
        }

        private class TestClass
        {
            public string DepartmentName;

            public int Salary;
        }
    }
}
using NUnit.Framework;

namespace GenRepo.Tests
{
    [TestFixture]
    public class SpecificationTests
    {
        [Test]
        public void Specification_is_satisfied_when_expression_is_true_for_candidate_object()
        {
            var candidateObject = new TestClass() {Salary = 5000};
            var specification = new Specification<TestClass>(t=>t.Salary>4000);

            var isSatisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(isSatisfied);
        }

        [Test]
        public void Specification_is_not_satisfied_when_expressions_is_false_for_candidate_object()
        {
            var candidateObject = new TestClass() { Salary = 3000 };
            var specification = new Specification<TestClass>(t => t.Salary > 4000);

            var isSatisfied = specification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(isSatisfied);
        }

        [Test]
        public void And_specification_is_satisfied_when_both_expressions_are_true_for_candidate_obect()
        {
            var candidateObject = new TestClass() { DepartmentName = "Sales", Salary = 5000 };
           
            var salarySpecification = new Specification<TestClass>(t => t.Salary > 4000);
            var departmentSpecification = new Specification<TestClass>(t=>t.DepartmentName=="Sales");
            var andSpecification = salarySpecification.And(departmentSpecification);

            var isSatisfied = andSpecification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(isSatisfied);
        }

        [Test]
        public void And_specification_is_not_satisfied_when_either_of_expression_is_false_for_candidate_obect()
        {
            var candidateObject = new TestClass() { DepartmentName = "Sales", Salary = 3000 };

            var salarySpecification = new Specification<TestClass>(t => t.Salary > 4000);
            var departmentSpecification = new Specification<TestClass>(t => t.DepartmentName == "Sales");
            var andSpecification = salarySpecification.And(departmentSpecification);

            var isSatisfied = andSpecification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(isSatisfied);
        }

        [Test]
        public void Or_specification_is_satisfied_when_either_of_expression_is_true_for_candidate_obect()
        {
            var candidateObject = new TestClass() { DepartmentName = "Sales", Salary = 3000 };

            var salarySpecification = new Specification<TestClass>(t => t.Salary > 4000);
            var departmentSpecification = new Specification<TestClass>(t => t.DepartmentName == "Sales");
            var orSpecification = salarySpecification.Or(departmentSpecification);

            var isSatisfied = orSpecification.IsSatisfiedBy(candidateObject);

            Assert.IsTrue(isSatisfied);
        }

        [Test]
        public void Or_specification_is_not_satisfied_when_both_expressions_are_false_for_candidate_obect()
        {
            var candidateObject = new TestClass() { DepartmentName = "Sales", Salary = 3000 };

            var salarySpecification = new Specification<TestClass>(t => t.Salary > 4000);
            var departmentSpecification = new Specification<TestClass>(t => t.DepartmentName == "HR");
            var orSpecification = salarySpecification.Or(departmentSpecification);

            var isSatisfied = orSpecification.IsSatisfiedBy(candidateObject);

            Assert.IsFalse(isSatisfied);
        }

        private class TestClass
        {
            public string DepartmentName;

            public int Salary;
        }
    }
}
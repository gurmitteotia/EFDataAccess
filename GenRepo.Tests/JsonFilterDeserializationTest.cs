﻿using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GenRepo.Tests
{
    [TestFixture]
    public class JsonFilterDeserializationTest
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
                new TestItem(14){Name = "Raja", Salary = 11000},
            }.AsQueryable();
        }

        [Test]
        public void Deserialize_filter_with_empty_json()
        {
            var jsonFilter = new JsonFilterExpression(@"{}");
            var f = jsonFilter.Filter<TestItem>();

            var filteredItems = f.Apply(_testItems).ToArray();
            Assert.That(filteredItems.Length, Is.EqualTo(5));
        }

        [Test]
        public void Deserialize_filter_with_one_greater_than_condition()
        {
            var d = @"{
			""Property"" : ""Salary"",
			""Operation"" : ""GreaterThan"",
			""Value"" : ""10000""
		}";

            var jsonFilter = new JsonFilterExpression(d);
            var f = jsonFilter.Filter<TestItem>();

            var filteredItems = f.Apply(_testItems).ToArray();

            Assert.That(filteredItems.Length, Is.EqualTo(3));
        }

        [Test]
        public void Deserialize_filter_with_one_less_than_condition()
        {
            var d = @"{
			""Property"" : ""Salary"",
			""Operation"" : ""LessThan"",
			""Value"" : ""10000""
		}";

            var jsonFilter = new JsonFilterExpression(d);
            var f = jsonFilter.Filter<TestItem>();

            var filteredItems = f.Apply(_testItems).ToArray();

            Assert.That(filteredItems.Length, Is.EqualTo(1));
        }


        [Test]
        public void Deserialize_combined_filter_with_and_operator()
        {
            var d = @"{
  ""LogicalOperator"" : ""And"",
  ""LHS"" : {
			""Property"" : ""Salary"",
			""Operation"" : ""GreaterThan"",
			""Value"" : ""10000""
	},
  ""RHS"" :{
  	    	""Property"" : ""Name"",
			""Operation"" : ""StartsWith"",
			""Value"" : ""Ra""
    }
}";
            var jsonFilter = new JsonFilterExpression(d);
            var f = jsonFilter.Filter<TestItem>();

            var filteredItems = f.Apply(_testItems).ToArray();

            Assert.That(filteredItems.Length, Is.EqualTo(1));
        }

        [Test]
        public void Deserialize_combined_filter_with_or_operator()
        {
            var d = @"{
  ""LogicalOperator"" : ""Or"",
  ""LHS"" : {
			""Property"" : ""Salary"",
			""Operation"" : ""GreaterThan"",
			""Value"" : ""10000""
	},
  ""RHS"" :{
			""Property"" : ""Name"",
			""Operation"" : ""StartsWith"",
			""Value"" : ""Ra""
	}  
}";
            var jsonFilter = new JsonFilterExpression(d);
            var f = jsonFilter.Filter<TestItem>();

            var filteredItems = f.Apply(_testItems).ToArray();

            Assert.That(filteredItems.Length, Is.EqualTo(4));
        }

        [Test]
        public void Deserialize_combined_filter_tree()
        {
            var d = @"{
  ""LogicalOperator"" : ""Or"",
  ""LHS"" : {
		""LogicalOperator"" : ""And"",
		""LHS"" : {
			""Property"" : ""Salary"",
	    	""Operation"" : ""GreaterThan"",
			""Value"" : ""10000""
		},
		""RHS"" :{
			""Property"" : ""Name"",
			""Operation"" : ""StartsWith"",
			""Value"" : ""Ra""
		}
  },
  ""RHS"" :{
	""Property"" : ""Name"",
	""Operation"" : ""StartsWith"",
	""Value"" : ""Shy""
  } 
}";
            var jsonFilter = new JsonFilterExpression(d);
            var f = jsonFilter.Filter<TestItem>();

            var filteredItems = f.Apply(_testItems).ToArray();

            Assert.That(filteredItems.Length, Is.EqualTo(2));
        }

        [Test]
        public void Throws_exception_for_invalid_logical_operator()
        {
            var d = @"{
  ""LogicalOperator"" : ""INVALID"",
  ""LHS"" : {
			""Property"" : ""Salary"",
			""Operation"" : ""GreaterThan"",
			""Value"" : ""10000""
	},
  ""RHS"" :{
  	    	""Property"" : ""Name"",
			""Operation"" : ""StartsWith"",
			""Value"" : ""Ra""
    }
}";
            var jsonFilter = new JsonFilterExpression(d);
            Assert.Throws<ArgumentException>(()=>jsonFilter.Filter<TestItem>());
        }
    }
}
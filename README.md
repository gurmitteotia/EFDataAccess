### EFDataAccess
A generic repository in C#.NET around EntityFramework with support for queries and sorting on known and dynamic fields.

### Description
EntityFramework has already done the good job of implementing Repository, Query and Metadata Mapping patterns. I have created a generic repository (more of a wrapper) on top of EntityFramework's repository with nice to have features.

### Features

1. **One repository to deal with all of the domain entities**: You can use an instance of generic repository to deal with all of your database entities. You do not need to inject different repositories in domain services, application services or controllers.
   ```cs
       
        var repository = new GenericRepository(new ScalableDataContext("Mapping.dll", "ConnectionName"))
        //add product
        repository.Add(new Product(){....})    
        //add customer
        repository.Add(new Customer(){....})
   ```
 You can read about ScaleableDataContext in [blog post](http://gurmitteotia.blogspot.co.uk/2015/07/entity-frameworks-entities-to-database.html)

2. **Uniform API to filter on known and dynamic fields**: 
   ```cs

       //Filter to query only keyboards with known field
       var keyboard = Filter<Product>.Create(p=>p.Category=="KeyBoard");
       //Get all keyboards
       var keyboards = repository.Get(keyboard)

       //You can combine filters
       var popular = Filter<Product>.Create(p=>p.Ratings > 4);
       var popularKeyboard = keyboard.And(popular);
       var popularKeyboards = repository.Get(popularKeyboard); 
       ...
      
       //You can create filters on dynamic field. It is useful when you are getting these fields from 
       //the search form to support the boolean search
       var keyboard = Filter<Product>.Create("Category", OperationType.EqualTo, "KeyBoard"); 
       //Combine dynamic queries
       var popular = Filter<Product>.Create ("Rating", OperationType.GreaterThan, 4)
       var popularKeyboards = repository.Get(keyboard.And(popular));
   ```
3. **Uniform API to support ordering on known and dynamic fields**:
   ```cs

        var orderedProduct = Query.Everything<Product>().OrderBy(o=>o.Asc(p=>p.Name));
        var orderedProducts = repository.Get(orderedProduct);
        ...
        
        //Order on dynamic field. It is usefull when user has the choice to sort the data in UI.
        var orderedProduct = Query.Everything<Product>().OrderBy(o=>o.Asc("Name"));
        var orderedProducts = repository.Get(orderedProduct)

        //You can build query with specific filters.
        var keyboard = Filter<Product>.Create(p=>p.Category=="KeyBoard");
        var orderedKeyboard = Query.WithFilter(keyboard).OrderBy(o=>o.Asc(p=>p.Name));
        var orderedProducts = repository.Get(orderedProduct)
        
   ```
4. **Understand Specification pattern**: You can use specification with generic repository. You can read about specification in [this paper](https://martinfowler.com/apsupp/spec.pdf) and my understanding of it at [StackOverflow](https://stackoverflow.com/questions/2506426/using-the-specification-pattern/32350270#32350270). I have also disambiguated the Specification and Query object patterns. Specification is not alternative to Query object pattern.
   ```cs
      var popularKeyboard = new Specification<Product>(p=>p.Rating>4);
      var popularKeyboards = repository.Get(popularKeyboard);
   ```

5. **Deserialization/serialization of filter expression tree**: You can deserialize the filter expressions from JSON representation.
   ```cs
        var d = @"{
			""Property"" : ""Category"",
			""Operation"" : ""EqualTo"",
			""Value"" : ""Keyboard""
		}";

        var jsonFilter = new JsonFilterExpression(d);
        var filter = jsonFilter.Filter<Product>();
        var filteredProduct = repository.Get(filter);
   ```
 You can deserialize a pretty complex filter expression, for more example please look at unit [test cases](https://github.com/gurmitteotia/EFDataAccess/blob/master/GenRepo.Tests/JsonFilterDeserializationTest.cs). You can create JSON representation by hand or by FilterExpression.Json as shown in following example:
   ```cs
    var lhs = new LeafFilterExpression()
            { Operation = OperationType.GreaterThan, Property = "Salary", Value = 10000 };

      var rhs = new LeafFilterExpression()
            { Operation = OperationType.Contains, Property = "Name", Value = "am" };

       var combined = new CompositeFilterExpression()
                {LHS = lhs, RHS = rhs, LogicalOperator = LogicalOperator.And};

      var jsonString = combined.Json();  
   ```
You can find more example in unit [test cases](https://github.com/gurmitteotia/EFDataAccess/blob/master/GenRepo.Tests/JsonFilterSerializationTest.cs)

6. **Supports projection**:  All query APIs on IRepository interface returns IEnumerable and not IQuerable. This will cause the EF query to be evaulated as soon as repository API is called. It is very usefull in MVC applications, where you can handle database error in controller rather then propagating them to views.
   ```cs
        var query = Query.Everything<Product>()
	           .ToProjection(p=>new {BrandName = p.Brand.Name, Id = p.Id, Name = p.Name});
        var productView = repository.Get(query);

   ```
  Please avoid following case as this will result in "Select" part being executed in C# code and not in SQL server:
   ```cs
      //Please avoid this.
      var query = Query.Everything<Product>()
      var productView = repository.Get(query)
             .Select(p=>new {BrandName = p.Brand.Name, Id = p.Id, Name = p.Name});
   ```

Note: Query/Filter objects are not tied to generic repository you can use them with EntityFramework repository of your style or with in-memory data.







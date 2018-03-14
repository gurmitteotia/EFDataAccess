### EFDataAccess
A generic repository in C#.NET around EntityFramework with support of queries and sorting on known and dynamic fields.

### Description
EntityFramework has already done the good job of implementing Repository, Query and Metadata Mapping patterns. I have created a generic repository on top of EntityFramework's repository with nice to have features.

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

2. **Uniform API to build the query for known and dynamic fields**: Though EntityFramework already implement Query pattern, by converting domain specific LINQ expression to SQL but I have created a Query object to deal with known and dynamic fields uniformly.
   ```cs

       //Create a simple query to filter keyboards with known field
       var keyboard = Query<Product>.Create(p=>p.Category=="KeyBoard");
       //Get all keyboards
       var keyboards = repository.Get(keyboard)

       //You can combine queries
       var popular = Query<Product>.Create(p=>p.Ratings > 4);
       var popularKeyboard = keyboard.And(popular);
       var popularKeyboards = repository.Get(popularKeyboard); 
       ...
      
       //You can create queries on dynamic field. It is useful when you are getting these fields from 
       //the search form to support the boolean search
       var keyboard = Query<Product>.Create("Category", OperationType.EqualTo, "KeyBoard"); 
       //Combine dynamic queries
       var popular = Qurey<Product>.Create ("Rating", OperationType.GreaterThan, 4)
       var popularKeyboards = repository.Get(keyboard.And(popular));
   ```
3. **Uniform API to support ordering for known and dynamic fields**:
   ```cs
        var keyboard = Query<Product>.Create(p=>p.Category=="KeyBoard");
        //Decorate this query to sort on name
        var orderedKeyboard = keyboard.OrderBy(o=>o.Asc(p=>p.Name));

        var orderedKeyboards = repository.Get(orderedKeyboard);
        
        ...
        
        //Decorate the query to sort on dynamic field. It is usefull when user has the choice to sort the data in UI.
        //Decorate this query to sort on dynamic field
        var orderedKeyboard = keyboard.OrderBy(o=>o.Asc("Name"));
        var orderedKeyboards = repository.Get(orderedKeyboards)

   ```

*Note*: [My blog](http://gurmitteotia.blogspot.co.uk/2015/06/generic-repository-around-entity.html) is not in sync with this code. I will update the blog soon.





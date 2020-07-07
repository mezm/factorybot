![Build and Run Tests](https://github.com/mezm/factorybot/workflows/Build%20and%20Run%20Tests/badge.svg?branch=master)

# FactoryBot
**FactoryBot** is a tool to generate test objects with random data. Inspired by factory_girl. 

Sometimes it may be really helpful for unit tests, performance test, any kind of proof of concept to generate random data.

For example:
```csharp
Bot.Define(x => new Model({ Text = x.Strings.Any() });
var model = Bot.Build<Model>();
```

Now **FactoryBot** supports generators for following types:
* `int`
* `long`
* `short`
* `byte`
* `double`
* `float`
* `decimal`
* `bool`
* `string`
* `DateTime`
* `TimeSpan`
* and any enum type

# Define Model Generators
**FactoryBot** support two models on defining model generation: manual and auto.

## Auto mode
In auto mode **FactoryBot** goes over all public properties with public setters and binds predefined generators to it. Here is how you can use it:
```csharp
Bot.DefineAuto<AllTypesModel>();
var model = Bot.Build<AllTypesModel>();
```

## Manual Mode
In manual mode you should specify a generator per each property you want to be set:
```csharp
Bot.Define(x => new AllTypesModel
{
    Boolean = x.Boolean.Any(),
    Byte = x.Byte.RandomFromList(3, 4, 5, 10, 12, 100),
    DateTime = x.Dates.AfterNow(),
    Decimal = x.Decimal.Any(-1000000, 1000000),
    Double = x.Double.SequenceFromList(12.45, 66.1234, 444.4),
    Enum = x.Enums.RandomFromList(EnumModel.First, EnumModel.Last),
    Float = x.Float.Any(),
    Integer = x.Integer.Any(0, 500),
    Long = x.Long.Any(),
    Short = x.Short.Any(-100, 100),
    String = x.Address.Country(),
    TimeSpan = x.Time.Any()
});

var model = Bot.Build<AllTypesModel>();
```

## Build Mutation
There is an ability to change one particular model during a build.

One way is to provide some modifiers to `Build` method:
```csharp
var model = Bot.Build<AllTypesModel>(x => x.Byte = 10, x => x.DateTime = DateTime.UtcNow);
```

Another way is to use `BuildCustom` method and provide define expression again to substitute some generators:
```csharp
var model = Bot.BuildCustom(x => new Model1(5, x.Keep<string>()));
```
Sometimes you need to change generator in constructor but you want to keep other ones untouched. For this call `Keep` method as it showed above.

# Other features
## Build Sequence
Sometimes it's required to generate array of models. It can be easily done `BuildSequence` method. The method returns `IEnumerable<T>` and works as [yield generator](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/yield) and generates infinite sequence of objects. To not creating infinite loops or generation `OutOfMemoryExceptions` in your programs the method has protection and if you try to generate more than 100 object you will get an exception. To disable this protection pass `true` as  *infinite* parameter.
```csharp
// generates sequence of 10 elements
Bot.BuildSequence<AllTypesModel>().Take(10).ToList().ForEach(x => Console.WriteLine(x)); 

// throws and error
foreach (var model in Bot.BuildSequence<AllTypesModel>()) 
{
  Console.WriteLine(model);
}

// creates an infinite loop
foreach (var model in Bot.BuildSequence<AllTypesModel>(true)) 
{
  Console.WriteLine(model);
}
```

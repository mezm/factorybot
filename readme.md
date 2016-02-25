FactoryBot
----------

FactoryBot is a tool to generate test objects with random data. Inspired by factory_girl.

```csharp
Bot.Define(x => new Model({ Text = x.Strings.Any() });
var model = Bot.Build<Model>();

```
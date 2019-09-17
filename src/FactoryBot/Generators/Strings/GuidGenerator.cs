using System;

namespace FactoryBot.Generators.Strings
{
    public class GuidGenerator : TypedGenerator<string>
    {
        protected override string NextInternal() => Guid.NewGuid().ToString();
    }
}
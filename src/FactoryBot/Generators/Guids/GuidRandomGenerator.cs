using System;

namespace FactoryBot.Generators.Guids
{
    public class GuidRandomGenerator : TypedGenerator<Guid>
    {
        protected override Guid NextInternal() => Guid.NewGuid();
    }
}

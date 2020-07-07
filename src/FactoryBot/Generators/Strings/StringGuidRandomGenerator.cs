using FactoryBot.Generators.Guids;

namespace FactoryBot.Generators.Strings
{
    public class StringGuidRandomGenerator : TypedGenerator<string>
    {
        private readonly GuidRandomGenerator _guidGenerator = new GuidRandomGenerator();

        protected override string NextInternal() => _guidGenerator.Next().ToString();
    }
}
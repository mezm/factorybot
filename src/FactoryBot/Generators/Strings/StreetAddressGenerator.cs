namespace FactoryBot.Generators.Strings
{
    public class StreetAddressGenerator : TypedGenerator<string>
    {
        private readonly IGenerator _nameGenerator = new FullNameGenerator();

        protected override string NextInternal()
        {
            var buildingNumber = NextRandomInteger(1, 9999);
            var streetName = _nameGenerator.Next();
            return $"{buildingNumber} {streetName} st.";
        }
    }
}

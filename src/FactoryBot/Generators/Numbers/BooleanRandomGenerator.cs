namespace FactoryBot.Generators.Numbers
{
    public class BooleanRandomGenerator : TypedGenerator<bool>
    {
        protected override bool NextInternal() => NextRandomBool();
    }
}
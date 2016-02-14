namespace FactoryBot.Generators.Numbers
{
    public class IntegerRandomGenerator : TypedGenerator<int>
    {
        private readonly int _from, _to;

        public IntegerRandomGenerator(int from = int.MinValue, int to = int.MaxValue)
        {
            Check.MinMax(from, to, nameof(from));

            _from = from;
            _to = to;
        }
        
        protected override int NextInternal()
        {
            return NextRandomInteger(_from, _to);
        }
    }
}
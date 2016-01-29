namespace FactoryBot.Generators.Numbers
{
    public class IntegerRandomGenerator : TypedGenerator<int>
    {
        private readonly int _from, _to;

        public IntegerRandomGenerator()
            : this(int.MinValue, int.MaxValue)
        {
        }

        public IntegerRandomGenerator(int from, int to)
        {
            _from = from;
            _to = to;
        }
        
        protected override int NextInternal()
        {
            return NextRandomInteger(_from, _to);
        }
    }
}
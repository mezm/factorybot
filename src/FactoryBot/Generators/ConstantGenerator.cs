namespace FactoryBot.Generators
{
    public class ConstantGenerator : IGenerator
    {
        private readonly object _constant;

        public ConstantGenerator(object constant)
        {
            _constant = constant;
        }

        public object Next()
        {
            return _constant;
        }
    }
}
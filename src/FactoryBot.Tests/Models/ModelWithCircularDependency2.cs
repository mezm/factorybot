namespace FactoryBot.Tests.Models
{
    public class ModelWithCircularDependency2
    {
        public ModelWithCircularDependency2(ModelWithCircularDependency3 model) => Model = model;

        public ModelWithCircularDependency3 Model { get; }
    }
}
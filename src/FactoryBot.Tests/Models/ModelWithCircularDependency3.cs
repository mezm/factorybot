namespace FactoryBot.Tests.Models
{
    public class ModelWithCircularDependency3
    {
        public ModelWithCircularDependency1 Model { get; set; }
    }
}
namespace FactoryBot.Tests.Models
{
    public class ModelWithCircularDependency1
    {
         public ModelWithCircularDependency2 Model { get; set; }
    }
}
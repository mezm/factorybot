namespace FactoryBot.Tests.Models
{
    public class NoPublicConstructorModel
    {
        private NoPublicConstructorModel()
        {
        }

        public string Text { get; set; }
    }
}

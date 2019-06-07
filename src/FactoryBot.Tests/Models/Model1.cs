namespace FactoryBot.Tests.Models
{
    public class Model1
    {
        public Model1()
        {
        }

        public Model1(int number) => Number = number;

        public Model1(int number, string text)
        {
            Number = number;
            Text = text;
        }

        public int Number { get; set; }

        public string Text { get; set; }
    }
}
using System.Collections.Generic;

namespace FactoryBot.Tests.Models
{
    public class Model4
    {
        public List<int> SimpleList { get; set; }
            
        public string[] SimpleArray { get; set; }

        public List<Model1> ComplexList { get; set; }
            
        public Model3[] ComplexArray { get; set; }

        public Dictionary<int, string> SimpleDictionary { get; set; }

        public Dictionary<Model1, Model2> ComplexDictionary { get; set; }
    }
}
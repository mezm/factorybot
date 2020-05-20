using System;
using System.Collections;
using System.Collections.Generic;

namespace FactoryBot.Generators.Collections
{
    internal class ListOfGenerators : IGenerator
    {
        private readonly Type _listType;
        private readonly IGenerator[] _generators;

        public ListOfGenerators(Type elementType, IGenerator[] generators)
        {
            _generators = generators;
            _listType = typeof(List<>).MakeGenericType(elementType);
        }

        public object Next()
        {
            var list = (IList)Activator.CreateInstance(_listType);
            foreach (var generator in _generators)
            {
                list.Add(generator.Next());
            }

            return list;
        }
    }
}
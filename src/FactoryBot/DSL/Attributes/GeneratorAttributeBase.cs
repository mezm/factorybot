using FactoryBot.Generators;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace FactoryBot.DSL.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class GeneratorAttributeBase : Attribute
    {
        public abstract IGenerator CreateGenerator(MethodInfo method, IDictionary<string, object> parameters);
    }
}
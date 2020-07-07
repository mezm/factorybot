using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FactoryBot.DSL.Builders;
using FactoryBot.ExpressionParser;

namespace FactoryBot
{
    /// <summary>
    /// Bot that can help you with auto generating you models
    /// </summary>
    public class Bot
    {
        /// <summary>
        /// Maximum number of item to generate by <see cref="BuildSequence{T}(bool)" /> before it throws an exception
        /// </summary>
        public const int SEQUENCE_MAX_LENGTH = 100;

        /// <summary>
        /// Builds a model based on configuration defined earlier for a type
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="modifiers">Optional build modifiers</param>
        /// <returns>Generated model</returns>
        public static T Build<T>(params Action<T>[] modifiers)
        {
            var result = (T)BotConfigurator.GetConfiguration(typeof(T)).CreateNewObject();
            foreach (var modifier in modifiers)
            {
                modifier(result);
            }

            return result;
        }

        /// <summary>
        /// Builds a model based on configuration defined earlier for a type with ability to alter configuration
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="constructorModifier">Configuration build modifier</param>
        /// <param name="afterConstructModifiers">Optional build modifiers</param>
        /// <returns>Generated model</returns>
        public static T BuildCustom<T>(
            Expression<Func<CustomConstructBuilder, T>> constructorModifier,
            params Action<T>[] afterConstructModifiers)
        {
            var parser = new ConstructorParser();
            var constructor = parser.Parse(constructorModifier);

            var result = (T)BotConfigurator.GetConfiguration(typeof(T)).CreateNewObjectWithModification(constructor);
            foreach (var modifier in afterConstructModifiers)
            {
                modifier(result);
            }

            return result;
        }

        /// <summary>
        /// Generates sequence of models
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="infinite">Whether it is an infinite sequence or not</param>
        /// <returns>Enumerable of generated models</returns>
        public static IEnumerable<T> BuildSequence<T>(bool infinite = false)
        {
            if (infinite)
            {
                while (true)
                {
                    yield return Build<T>();
                }
            }

            for (var i = 0; i < SEQUENCE_MAX_LENGTH; i++)
            {
                yield return Build<T>();
            }

            throw new InvalidOperationException(
                "BuildSequence generates infinite sequence and should not be used in a foreach loop. If it's not the case set infinite parameter to true.");
        }
    }
}

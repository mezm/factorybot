using System;
using System.IO;
using System.Reflection;
using FactoryBot.Extensions;
using FactoryBot.Generators;

namespace FactoryBot.Tests
{
    internal static class FileUtils
    {
        public static string GetResourceContentWithoutLineBreaks(string resourceName)
        {
            var assembly = Assembly.GetAssembly(typeof(IGenerator));
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new IOException($"Resource {resourceName} has not been found in assembly {assembly}.");
            }

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd().RemoveLineBreaks();
            }
        }

        public static void WithFileDisposal(string filename, Action action)
        {
            try
            {
                action();
            }
            finally
            {
                File.Delete(filename);
            }
        }
    }
}
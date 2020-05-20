using System;
using System.IO;
using System.Reflection;

namespace FactoryBot.Utils
{
    public static class ResourceHelper
    {
        public static Stream OpenStream(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException("String should not be null or empty.", nameof(resourceName));
            }

            var assembly = Assembly.GetCallingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new IOException($"Resource {resourceName} has not been found in assembly {assembly}.");
            }

            return stream;
        }

        public static TResult Read<TResult>(string resourceName, Func<StreamReader, TResult> read)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException("String should not be null or empty.", nameof(resourceName));
            }

            var stream = OpenStream(resourceName);
            using (var reader = new StreamReader(stream))
            {
                return read(reader);
            }
        }

        public static long GetStreamLength(string resourceName)
        {
            if (string.IsNullOrWhiteSpace(resourceName))
            {
                throw new ArgumentException("String should not be null or empty.", nameof(resourceName));
            }

            using var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new IOException($"Resource {resourceName} has not been found.");
            }

            return stream.Length;
        }
    }
}
using System;
using System.IO;
using System.Reflection;

namespace FactoryBot.Utils
{
    public static class ResourceHelper
    {
        public static Stream OpenStream(string resourceName)
        {
            Check.NotNullOrWhiteSpace(resourceName, nameof(resourceName));

            var assembly = Assembly.GetCallingAssembly();
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                throw new IOException($"Resource {resourceName} has not been found in assembly {assembly}.");
            }

            return stream;
        }

        public static TResult Read<TResult>(string resourceName, Func<Stream, StreamReader, TResult> read)
        {
            Check.NotNullOrWhiteSpace(resourceName, nameof(resourceName));
            Check.NotNull(read, nameof(read));

            var stream = OpenStream(resourceName);
            using (var reader = new StreamReader(stream))
            {
                return read(stream, reader);
            }
        }

        public static long GetStreamLength(string resourceName)
        {
            Check.NotNullOrWhiteSpace(resourceName, nameof(resourceName));

            using (var stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    throw new IOException($"Resource {resourceName} has not been found.");
                }

                return stream.Length;
            }
        }
    }
}
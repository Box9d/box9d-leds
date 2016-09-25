using System;
using System.IO;

namespace Box9.Leds.Core.Validation
{
    public static class Guard
    {
        public static void NotNullOrEmpty<T>(T obj)
        {
            if (obj == null || obj.ToString() == string.Empty)
            {
                throw new ArgumentException("Argument cannot be null or empty");
            }
        }

        public static void FileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException(string.Format("File '{0}' does not exist", filePath));
            }
        }
    }
}

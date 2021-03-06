﻿namespace iPodcastSearch
{
    using System.IO;
    using System.Runtime.Serialization.Json;
    using System.Text;

    public static class JsonHelper
    {
        public static string Serialize<T>(T data)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, data);
                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T Deserialize<T>(string objString)
        {
            using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(objString)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T) serializer.ReadObject(stream);
            }
        }
    }
}
﻿using System.Text.Json;

namespace StoriesHackerNews.Extension
{
    public class JsonHelper
    {
        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}

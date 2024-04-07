using Newtonsoft.Json;
using RestSharp;
using System.IO;

namespace API
{
    public static class HandleContent
    {
        public static string SerializeJson(dynamic payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.Indented);
        }

        //Here the content can be anything so we are making it generic.
        //If you put T in place of return data type then <T> must also be written
        public static T GetContent<T>(RestResponse response)
        {
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }
        //For big Json files
        public static T ParseJson<T>(string fileName)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(fileName));
        }
    }
}

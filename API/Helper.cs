using RestSharp;
using System.IO;

namespace API
{
    public class Helper
    {
        private RestClient client;
        private RestRequest request;
        private const string baseurl = "https://reqres.in/";
        private string url;

        public RestClient SetUrl(string endpoint)
        {
            url = Path.Combine(baseurl, endpoint);
            client = new RestClient(url);
            return client;
        }

        public RestRequest CreateGetRequest()
        {
            request = new RestRequest(url, Method.Get);
            request.AddHeader("Accept", "application/json");
            return request;
        }

        public RestResponse GetResponse(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }


        //post method
        public RestRequest CreatePostRequest(string payload)
        {
            request = new RestRequest(url, Method.Post);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", payload, ParameterType.RequestBody);
            return request;
        }

        //put request
        public RestRequest CreatePutRequest(string payload)
        {
            request = new RestRequest(url, Method.Put);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", payload, ParameterType.RequestBody);
            return request;
        }

        //Delete request
        public  RestRequest CreateDeleteRequest()
        {
            request = new RestRequest(url,Method.Delete);
            request.AddHeader("Accept", "application/json");
            return request;
        }
    }
}

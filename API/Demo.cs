using API.Models;
using RestSharp;

namespace API
{
    public class Demo
    {

        //                      This is the first iteration then I refactored code to below method
        //public Users GetUsers()
        //{
        //    //creating a new client that will have the request to send
        //    var client = new RestSharp.RestClient("https://reqres.in/");
        //    // this will have the end point
        //    var request = new RestRequest("api/users?page=2", Method.Get);
        //    request.AddHeader("Accept","Application/Json");
        //    //defining the content type i.e. json
        //    request.RequestFormat = DataFormat.Json;

        //    //getting the response and retriving the body
        //    var response = client.Execute(request);
        //    var content = response.Content;
        //    //this is from newtonsoft.json - this will give json objects
        //    Users user = JsonConvert.DeserializeObject<Users>(content);
        //    return user;
        //}

        //Now I am going to create a method to get response from Post method
        //So again I need to create object for Helper. Which is not good.
        //So I can create a constructor and then that constructor will call create a global object

        private Helper helper;
        public Demo()
        {
            helper = new Helper();
        }

        public Users GetUsers()
        {
            //below line is commented cuz line 35
            //var helper = new Helper();
            var client = helper.SetUrl("api/users?page=2");
            var request = helper.CreateGetRequest();
            request.RequestFormat = DataFormat.Json;
            var response = helper.GetResponse(client, request);
            var users = HandleContent.GetContent<Users>(response);
            return users;
        }



        public CreateUserResponse CreateNewUser(string payload)
        {
            var client = helper.SetUrl("api/users");
            var request = helper.CreatePostRequest(payload);
            var response = helper.GetResponse(client, request);
            var createUser = HandleContent.GetContent<CreateUserResponse>(response);
            return createUser;
        }

        public CreateUserResponse CreateNewUserNew(dynamic payload)
        {
            var client = helper.SetUrl("api/users");
            //Since the payload is dynamic we need to serialize it 
            string json = HandleContent.SerializeJson(payload);
            var request = helper.CreatePostRequest(json);
            var response = helper.GetResponse(client, request);
            var createUser = HandleContent.GetContent<CreateUserResponse>(response);
            return createUser;
        }
    }
}

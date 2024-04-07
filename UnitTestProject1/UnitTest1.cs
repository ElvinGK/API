using API;
using API.Models.Requests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public TestContext TestContext { get; set; }
        //This is to test a get method
        [TestMethod]
        public void TestMethod1()
        {
            //create an object of Demo after adding reference
            var api = new Demo();
            var response = api.GetUsers();
            Assert.AreEqual(2, response.page);
        }

        //This is to test a post method. The body is given as a string here
        //but it will fail as I have not given body
        [TestMethod]
        public void TestMethod2()
        {
            //Here we need to give the body in  strings but this cant be used when body is complex
            var payload = "";
            var api = new Demo();
            var response = api.CreateNewUser(payload);
            Assert.AreEqual("morpheus", response.name);
        }


        //This is to test a post method. The body is given from a model class here
        //This below line is required to call the test data before the test runs
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
            "|DataDirectory|\\TestData\\CreateUser.csv", "CreateUser.csv", DataAccessMethod.Sequential),
            DeploymentItem("TestData\\CreateUser.csv"), TestMethod]
        public void TestMethod3()
        {
            //var user = new CreateUserRequest();
            //user.name = TestContext.DataRow["Name"].ToString();
            //user.job = TestContext.DataRow["Job"].ToString();
            //Below is the better way to create user and testcontext needs to be used here to call
            //csv file. I wrote line 11 for this test only.
            var user = new CreateUserRequest
            {
                name = TestContext.DataRow["Name"].ToString(),
                job = TestContext.DataRow["Job"].ToString()
            };
            var api = new Demo();
            //Had to create another method because user is an object here and a serialization method was 
            //created.
            var response = api.CreateNewUserNew(user);
            Assert.AreEqual(user.name, response.name);

        }
        //if I give TestData only here then all the files under this folder is available
        [DeploymentItem("TestData")]
        [TestMethod]
        public void ComplexPostTest()
        {
            //Create a json file in test data and paste the body there
            var payload = HandleContent.ParseJson<CreateUserRequest>("CreateUser.Json");
            var api = new Demo();
            var response = api.CreateNewUserNew(payload);
            Assert.AreEqual(payload.name, response.name);
        }
    }
}

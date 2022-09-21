using RestSharp;
using System.Net;
using RestSharp.Serializers.Json;
using Microsoft.Extensions.Options;

namespace TestUserApi
{
    public class Tests
    {
        private RestClient _client;

        private const string BASE_URL = "https://localhost:9001/api/Users";

        private string ExpectedValue = "Sihle";

        [OneTimeSetUp]
        public void SetUpRestSharpClient()
        {
            _client = new RestClient(BASE_URL);
        }
        [Test]
        public void TestUsers()
        {
            RestRequest request = (RestRequest) new RestRequest("/", Method.Get);

            RestResponse response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        [Test]
        public void TestUser()
        {
            RestRequest request = (RestRequest)new RestRequest("/{id}", Method.Get).AddUrlSegment("id",1);

            RestResponse response = _client.Execute(request);

            SystemTextJsonSerializer txt = new SystemTextJsonSerializer();
            UserJSON user = txt.Deserialize<UserJSON>(response);

            Assert.That(user.UserName, Is.EqualTo(ExpectedValue));
        }

        [Test]
        public void TestSaveUser()
        {
            UserJSON json = new UserJSON
            {
                Id = 7,
                UserName = "Theressa"
            };

            RestRequest request = (RestRequest)new RestRequest("/", Method.Post).AddJsonBody(json);

            RestResponse response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
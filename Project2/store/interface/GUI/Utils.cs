using RestSharp;

namespace @interface
{
    public static class Utils
    {
        private const string UrlDefault = "http://localhost/api/store/";

        // Auth routes
        public const string Login = UrlDefault + "login";

        // Book routes
        public const string Books = UrlDefault + "books";

        // Clients routes
        public const string Clients = UrlDefault + "clients";

        // Orders routes
        public const string Orders = UrlDefault + "orders";

        // Receive Stock routes
        public const string ReceiveStock = UrlDefault + "receiveStock";

        // Sells routes
        public const string Sells = UrlDefault + "sells";

        // Top Books
        public const string Statistics = UrlDefault + "statistics";

        public static string Token = "";

        public const string PusherKey = "dd7136ca250e4358b688";

        //Handles login request
        public static IRestResponse ExecuteAuthRequest(string url, string body)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddParameter("application/json", body, ParameterType.RequestBody);

            return client.Execute(request);
        }

        //Handles get and put requests
        public static IRestResponse ExecuteRequest(string url, Method method, string id, string body)
        {
            if (!id.Equals(""))
                url += "/" + id;

            var client = new RestClient(url);
            var request = new RestRequest(method);

            request.AddHeader("Authorization", "Bearer " + Token);

            if (method != Method.GET)
                request.AddParameter("application/json", body, ParameterType.RequestBody);

            return client.Execute(request);
        }

        //Handles post requests
        public static IRestResponse ExecutePostRequest(string url, string body)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);

            request.AddHeader("Authorization", "Bearer " + Token);
            request.AddParameter("application/json", body, ParameterType.RequestBody);

            return client.Execute(request);
        }
    }
}
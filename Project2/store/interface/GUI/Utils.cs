using RestSharp;

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

    public static string Token =
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7ImlkIjoxLCJlbWFpbCI6ImVtcGxveWVlQHN0b3JlLmNvbSIsInJvbGUiOiJFTVBMT1lFRSJ9LCJpYXQiOjE1NTgyNjQ2Njl9.s580jaLZBpJPHcLreTdm2Wlw8nEr35h1gjfOzmKphgg";

    public const string PusherKey = "dd7136ca250e4358b688";

    enum MessageType
    {
        request_stock,
        receive_stock
    }

    public const string AMQP_QUEUE_REQUEST_STOCK = "request_stock";
    public const string AMQP_QUEUE_RECEIVE_STOCK = "receive_stock";
    public const string PUSHER_CHANNEL_STORE = "store";

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

        if (method == Method.PUT)
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
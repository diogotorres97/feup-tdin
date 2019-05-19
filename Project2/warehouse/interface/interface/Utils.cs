using RestSharp;
using System;

public class Utils
{
    const string urlDefault = "http://localhost/api/warehouse/";

    // Auth routes
    public const string login = urlDefault + "login";

    // Book routes
    public const string books = urlDefault + "books";

    // Receive Stock routes
    public const string requests = urlDefault + "requests";

    public static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7ImlkIjoxLCJlbWFpbCI6ImVtcGxveWVlQHN0b3JlLmNvbSIsInJvbGUiOiJFTVBMT1lFRSJ9LCJpYXQiOjE1NTgyOTI5ODN9.luB3Bxq0v14WSaNMVXulV8VnhtOyeeKnuDAbcMnTFBs";

    public const string AppKey = "dd7136ca250e4358b688";

    enum MessageType { request_stock, receive_stock };

    public const string AMQP_QUEUE_REQUEST_STOCK = "request_stock";
    public const string AMQP_QUEUE_RECEIVE_STOCK = "send_stock";
    public const string PUSHER_CHANNEL_WAREHOUSE = "warehouse";

    public Utils()
    {

    }

    public static IRestResponse executeAuthRequest(string url, string id, Method method, string parameters)
    {
        if (!id.Equals(""))
            url += "/" + id;



        var client = new RestClient(url);
        var request = new RestRequest(method);
        request.AddHeader("content-length", parameters.Length.ToString());
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("undefined", parameters, ParameterType.RequestBody);

        return client.Execute(request);
    }

    public static IRestResponse executeRequestRequest(string url, string id, Method method, string parameters)
    {
        if (!id.Equals(""))
            url += "/" + id + "/sendStock";

        var client = new RestClient(url);
        var request = new RestRequest(method);

        request.AddHeader("content-length", parameters.Length.ToString());
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Authorization", "Bearer " + token);
        request.AddParameter("undefined", parameters, ParameterType.RequestBody);

        return client.Execute(request);
    }

    public static IRestResponse executeRequest(string url, string id, Method method, string parameters)
    {
        if (!id.Equals(""))
            url += "/" + id;

        var client = new RestClient(url);
        var request = new RestRequest(method);

        Console.WriteLine(url);
        Console.WriteLine(parameters);

        request.AddHeader("content-length", parameters.Length.ToString());
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Authorization", "Bearer " + token);
        request.AddParameter("undefined", parameters, ParameterType.RequestBody);

        return client.Execute(request);
    }

}

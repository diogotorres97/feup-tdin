using RestSharp;
using System;

public class Utils
{
    const string urlDefault = "http://localhost/api/store/";

    // Auth routes
    public const string login = urlDefault + "login";

    // Book routes
    public const string books = urlDefault + "books";

    // Clients routes
    public const string clients = urlDefault + "clients";

    // Orders routes
    public const string orders = urlDefault + "orders";

    // Receive Stock routes
    public const string receiveStock = urlDefault + "receiveStock";

    // Sells routes
    public const string sells = urlDefault + "sells";

    public static string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VyIjp7ImlkIjozLCJlbWFpbCI6ImpvaG5Ac3RvcmUuY29tIn0sImlhdCI6MTU1ODI2MTk1Mn0.XKHE3ySbep5on2TVrSd0lGn2uMM2BqivIK04blgfhqU";

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

    public static IRestResponse executeRequest(string url, string id, Method method, string parameters)
    {
        if (!id.Equals(""))
            url += "/" + id;

        var client = new RestClient(url);
        var request = new RestRequest(method);
        request.AddHeader("content-length", parameters.Length.ToString());
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddHeader("Authorization", "Bearer " + token);
        request.AddParameter("undefined", parameters, ParameterType.RequestBody);

        return client.Execute(request);
    }

}

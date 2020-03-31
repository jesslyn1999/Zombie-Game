using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class RestApi
{
    private static readonly HttpClient client = new HttpClient();
    private const string HTTP_BASE_URL = "http://134.209.97.218:5051/scoreboards/13517053";

    public static async Task PostScore(string username, int score)
    {
        try
        {
            Debug.Log("Post score to server with username=" + username + " and score=" + score);
            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "username", username },
                { "score", score.ToString() }
            };

            HttpContent content = new FormUrlEncodedContent(values);
            Debug.Log("get contend : " + content.ToString());
            HttpResponseMessage response = await client.PostAsync(HTTP_BASE_URL, content);
            response.EnsureSuccessStatusCode();
            string responseString = await response.Content.ReadAsStringAsync();
            Debug.Log("Done Post Request with responseString=" + responseString);
        }
        catch (HttpRequestException exc)
        {
            Debug.Log("Error");
            Debug.LogError("Exception in post request: " + exc.Message);
        }
    }

    public static async Task<Dictionary<string, string>[]> GetScores()
    {
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try
        {
            string responseBody = await client.GetStringAsync(HTTP_BASE_URL);
            return JsonConvert.DeserializeObject<Dictionary<string, string>[]>(responseBody);
        }
        catch (HttpRequestException exc)
        {
            Debug.LogError("Exception in get request: " + exc.Message);
            return new Dictionary<string, string>[0];
        }
    }
}

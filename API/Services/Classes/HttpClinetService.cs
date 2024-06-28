using System.Net;

namespace API.Services.Classes
{
    public static class HttpClinetService
    {
       public static async Task<bool> UrlExists(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    return response.StatusCode==HttpStatusCode.OK;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

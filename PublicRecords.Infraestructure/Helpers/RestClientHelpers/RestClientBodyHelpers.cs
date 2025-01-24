using RestSharp;

namespace PublicRecords.Infraestructure.Helpers.RestClientHelpers
{
    internal class RestClientBodyHelpers
    {
        internal static async Task<RestResponse?> GetOfficialStateDiary(Dictionary<string, string> queryBody, string client)
        {
            var request = CreateHttpRequestBody(queryBody, client);

            var response = await SendOfficialDiaryRequest(request);

            return GetResponseContentQuery(response);
        }

        internal static RestRequest CreateHttpRequestBody(Dictionary<string, string> queryBody, string client)
        {
            var request = new RestRequest(client)
            {
                Method = Method.Post
            };

            foreach (var (key, value) in queryBody.Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)))
                request.AddParameter(key, value);

            request.AlwaysMultipartFormData = true;

            return request;
        }

        internal static async Task<RestResponse> SendOfficialDiaryRequest(RestRequest request)
        {
            var client = new RestClient();

            var response = await client.ExecuteAsync(request);

            return response;
        }

        internal static RestResponse? GetResponseContentQuery(RestResponse response)
        {
            if (string.IsNullOrWhiteSpace(response.Content))
                return null;

            return response;
        }

    }
}

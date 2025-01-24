using RestSharp;

namespace PublicRecords.Infraestructure.Helpers.RestClientHelpers
{
    internal static class RestClientQueryHelpers
    {
        internal static async Task<RestResponse?> GetOfficialMunicipalDiary(Dictionary<string, string> queryBody, string client)
        {
            var request = CreateHttpRequestQuery(queryBody, client);

            var response = await SendOfficialDiaryRequest(request);

            return GetResponseContentQuery(response);
        }

        internal static RestRequest CreateHttpRequestQuery(Dictionary<string, string> queryBody, string client)
        {
            var request = new RestRequest(client)
            {
                Method = Method.Get
            };

            foreach (var (key, value) in queryBody.Where(x => !string.IsNullOrEmpty(x.Key) && !string.IsNullOrEmpty(x.Value)))
            {
                request.AddQueryParameter(key, value);
            }

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

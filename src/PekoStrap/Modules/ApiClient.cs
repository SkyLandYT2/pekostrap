using System;

using System.Collections.Generic;

using System.Linq;

using System.Net;

using System.Net.Http;

using System.Threading.Tasks;

using Newtonsoft.Json.Linq;



namespace PekoraStrap.Modules

{

    public class ApiClient

    {

        private readonly HttpClient _httpClient;

        private readonly HttpClientHandler _httpClientHandler;

        private readonly Dictionary<string, string> _placeNameCache;

        private readonly Dictionary<string, string> _imageUrlCache;

        private readonly Dictionary<string, string> _userInfoCache;

        private readonly Dictionary<string, string> _headshotUrlCache;

        private bool _tokenInvalid;

        private const string API_URL = "https://www.pekora.zip/apisite/games/v1/games/multiget-place-details";

        private const string THUMBNAIL_API_URL = "https://www.pekora.zip/apisite/thumbnails/v1/games/icons?size=150x150&format=png&universeIds=";

        private const string USER_API_URL = "https://www.pekora.zip/apisite/users/v1/users/authenticated";

        private const string HEADSHOT_API_URL = "https://www.pekora.zip/apisite/thumbnails/v1/users/avatar-headshot?userIds=";

        private const string BASE_URL = "https://www.pekora.zip";



        public ApiClient()

        {

            _httpClientHandler = new HttpClientHandler

            {

                AllowAutoRedirect = false,

                UseCookies = true,

                CookieContainer = new CookieContainer()

            };

            _httpClient = new HttpClient(_httpClientHandler) { Timeout = TimeSpan.FromSeconds(5) };

            _placeNameCache = new Dictionary<string, string>();

            _imageUrlCache = new Dictionary<string, string>();

            _userInfoCache = new Dictionary<string, string>();

            _headshotUrlCache = new Dictionary<string, string>();

            SetupHttpClient();

        }



        private void SetupHttpClient()

        {

            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Safari/537.36");

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        }



        public async Task<(string placeName, string imageUrl)> GetPlaceNameAndImage(string placeId, string token)

        {

            if (_placeNameCache.TryGetValue(placeId, out string cachedName) && _imageUrlCache.TryGetValue(placeId, out string cachedImage))

                return (cachedName, cachedImage);



            if (string.IsNullOrEmpty(token))

            {

                _placeNameCache[placeId] = $"Place ID: {placeId}";

                _imageUrlCache[placeId] = null;

                return (_placeNameCache[placeId], _imageUrlCache[placeId]);

            }



            try

            {

                _httpClientHandler.CookieContainer.Add(new Uri(BASE_URL), new Cookie(".PEKOSECURITY", token));

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);



                var response = await _httpClient.GetAsync($"{API_URL}?placeIds={placeId}");

                if (response.StatusCode == System.Net.HttpStatusCode.Found)

                {

                    _placeNameCache[placeId] = $"Place ID: {placeId}";

                    _imageUrlCache[placeId] = null;

                    return (_placeNameCache[placeId], _imageUrlCache[placeId]);

                }

                response.EnsureSuccessStatusCode();

                var data = JArray.Parse(await response.Content.ReadAsStringAsync());

                string placeName = $"Place ID: {placeId}";

                string universeId = null;

                if (data.Count > 0)

                {

                    placeName = data[0]["name"]?.ToString() ?? $"Place ID: {placeId}";

                    universeId = data[0]["universeId"]?.ToString();

                }



                string imageUrl = null;

                if (universeId != null)

                {

                    var thumbnailResponse = await _httpClient.GetAsync($"{THUMBNAIL_API_URL}{universeId}");

                    if (thumbnailResponse.StatusCode != System.Net.HttpStatusCode.Found)

                    {

                        thumbnailResponse.EnsureSuccessStatusCode();

                        var thumbnailData = JObject.Parse(await thumbnailResponse.Content.ReadAsStringAsync());

                        if (thumbnailData["data"]?.Count() > 0)

                            imageUrl = thumbnailData["data"][0]["imageUrl"]?.ToString();

                    }

                }



                _placeNameCache[placeId] = placeName;

                _imageUrlCache[placeId] = imageUrl;

                return (placeName, imageUrl);

            }

            catch (HttpRequestException ex)

            {

                if (ex.Message.Contains("401") || ex.Message.Contains("403"))

                {

                    _tokenInvalid = true;

                }

            }

            catch

            {

                // Silently ignore errors

            }



            _placeNameCache[placeId] = $"Place ID: {placeId}";

            _imageUrlCache[placeId] = null;

            return (_placeNameCache[placeId], _imageUrlCache[placeId]);

        }



        public async Task<(string userId, string username, string headshotUrl)> GetUserInfoAndHeadshot(string token)

        {

            if (_userInfoCache.TryGetValue("id", out string userId) && _userInfoCache.TryGetValue("name", out string username) && _headshotUrlCache.TryGetValue("url", out string headshotUrl))

                return (userId, username, headshotUrl);



            if (string.IsNullOrEmpty(token))

            {

                return (null, null, null);

            }



            try

            {

                _httpClientHandler.CookieContainer.Add(new Uri(BASE_URL), new Cookie(".PEKOSECURITY", token));

                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);



                var response = await _httpClient.GetAsync(USER_API_URL);

                if (response.StatusCode == System.Net.HttpStatusCode.Found)

                {

                    return (null, null, null);

                }

                response.EnsureSuccessStatusCode();

                var data = JObject.Parse(await response.Content.ReadAsStringAsync());

                userId = data["id"]?.ToString();

                username = data["name"]?.ToString();



                string headshotUrlResult = null;

                if (userId != null)

                {

                    var headshotResponse = await _httpClient.GetAsync($"{HEADSHOT_API_URL}{userId}");

                    if (headshotResponse.StatusCode != System.Net.HttpStatusCode.Found)

                    {

                        headshotResponse.EnsureSuccessStatusCode();

                        var headshotData = JObject.Parse(await headshotResponse.Content.ReadAsStringAsync());

                        if (headshotData["data"]?.Count() > 0)

                        {

                            headshotUrlResult = headshotData["data"][0]["imageUrl"]?.ToString();

                            if (headshotUrlResult?.StartsWith("/") == true)

                                headshotUrlResult = $"{BASE_URL}{headshotUrlResult}";

                        }

                    }

                }



                _userInfoCache["id"] = userId;

                _userInfoCache["name"] = username;

                _headshotUrlCache["url"] = headshotUrlResult;

                return (userId, username, headshotUrlResult);

            }

            catch (HttpRequestException ex)

            {

                if (ex.Message.Contains("401") || ex.Message.Contains("403"))

                {

                    _tokenInvalid = true;

                }

            }

            catch

            {

                // Silently ignore errors

            }



            _userInfoCache["id"] = null;

            _userInfoCache["name"] = null;

            _headshotUrlCache["url"] = null;

            return (null, null, null);

        }



        public bool IsTokenInvalid => _tokenInvalid;

    }

}
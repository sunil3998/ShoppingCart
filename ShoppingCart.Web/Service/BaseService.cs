using Newtonsoft.Json;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.Service.IService;
using ShoppingCart.Web.Utility;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using static ShoppingCart.Web.Utility.StaticData;


namespace ShoppingCart.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProviderService _tokenProviderService;
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProviderService tokenProviderService)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProviderService = tokenProviderService;
        }
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer=true)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("ShoppingCartApi");
                HttpRequestMessage httpRequestMessage = new();
                httpRequestMessage.Headers.Add("Accept", "Application/json");

                //token
                if(withBearer)
                {
                    var token = _tokenProviderService.GetToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        httpRequestMessage.Headers.Add("Authorization", $"Bearer {token}");
                    }
    
                }
                 
                httpRequestMessage.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }
                HttpResponseMessage? httpResponseMessage = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        httpRequestMessage.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        httpRequestMessage.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        httpRequestMessage.Method = HttpMethod.Delete;
                        break;
                    default:
                        httpRequestMessage.Method = HttpMethod.Get;
                        break;
                }

                httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                switch (httpResponseMessage.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess=false
                };
                return dto;
            }
        }
    }
}

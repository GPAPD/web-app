using System.Net.Http;
using System.Net;
using System.Text;
using web_app.Models;
using web_app.Service.IService;
using static web_app.Utility.SD;
using web_app.Utility;
using Newtonsoft.Json;

namespace web_app.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = false)
        {
            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient("PRAVIX_MATIC");
                HttpRequestMessage message = new();
                message.Headers.Add("Accsepts", "application/json");

                //token
                if (withBearer)
                {

                }
                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiRsponse = null;

                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                apiRsponse = await httpClient.SendAsync(message);

                switch (apiRsponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new ResponseDto() { IsSuccess = false, Message = "NotFound" };
                    case HttpStatusCode.Forbidden:
                        return new ResponseDto() { IsSuccess = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new ResponseDto() { IsSuccess = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new  () { IsSuccess = false, Message = "InternalServerError" };
                    default:
                        var apiContent = await apiRsponse.Content.ReadAsStringAsync();

                        ResponseDto apiResponseDto;

                        if (requestDto.Url.Contains(SD.FlaskBackedApi + "/semanticSearch"))
                        {
                            var data = JsonConvert.DeserializeObject<SearchResponse>(apiContent);
                            if (data != null)
                            {
                                apiResponseDto = new ResponseDto() { IsSuccess = true, Result = data, Message = "featch Successed" };
                                return apiResponseDto;
                            }

                        }
                        if (requestDto.Url.Contains(SD.FlaskBackedApi + "/vcDatabase"))
                        {
                            var data = JsonConvert.DeserializeObject<IndexingRes>(apiContent);
                            if (data != null)
                            {
                                apiResponseDto = new ResponseDto() { IsSuccess = true, Result = data, Message = "featch Successed" };
                                return apiResponseDto;
                            }

                        }

                        apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                        return apiResponseDto;

                }
            }
            catch (Exception ex)
            {
                var dto = new ResponseDto()
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };

                return dto;
            }
        }
    }
}

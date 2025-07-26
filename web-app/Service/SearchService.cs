using web_app.Models;
using web_app.Service.IService;
using web_app.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace web_app.Service
{
    public class SearchService : ISearchService
    {
        private readonly IBaseService _baseService;
        public SearchService(IBaseService baseService) 
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto> GetSearchData(object date)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = date,
                Url = SD.FlaskBackedApi + "/semanticSearch"
            }, false);
        }

        public async Task<ResponseDto> UpdateIndexing()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.FlaskBackedApi + "/vcDatabase"
            }, false);
        }
    }
}

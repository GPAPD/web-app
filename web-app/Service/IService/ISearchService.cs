using web_app.Models;

namespace web_app.Service.IService
{
    public interface ISearchService
    {
        public Task<ResponseDto> GetSearchData(object data);

        public Task<ResponseDto> UpdateIndexing();
    }
}

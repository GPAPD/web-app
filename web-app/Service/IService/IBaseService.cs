using web_app.Models;

namespace web_app.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto, bool withBearer = true);
    }

    
}

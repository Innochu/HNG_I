using HNG.Domain.AuthEntities;
using HNG.Domain.BaseEntities;
namespace HNG.Application.Interface
{
    public interface IAuthService
    {
            Task<ApiResponse<string>> LoginAsync(Login loginDTO);
        
    }
}

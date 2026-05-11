namespace ClientTicketingSystem.Application.Services.Interfaces;
public interface IAuthServices
{
    Task<ApiResponse<string>> LoginAsync(LoginDto request);

}

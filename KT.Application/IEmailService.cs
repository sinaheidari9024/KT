using KT.Application.DTOs;

namespace KT.Infrastructure.Data
{
    public interface IEmailService
    {
        Task SendOTPAsync(SendMessageDTO sendEmailDTO);
    }
}
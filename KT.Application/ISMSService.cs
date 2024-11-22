using KT.Application.DTOs;

namespace KT.Infrastructure.Data
{
    public interface ISMSService
    {
        Task SendOTPAsync(SendMessageDTO sendEmailDTO);
    }
}
namespace KT.Infrastructure.Data
{
    public interface IMessageService
    {
        Task SendOTPAsync(SendMessageDTO model);
    }
}

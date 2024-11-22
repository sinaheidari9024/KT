using KT.Application.DTOs;

namespace KT.Infrastructure.Data
{
    public class MessageService : IMessageService
    {
        private readonly IServiceProvider serviceProvider;

        public MessageService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        } // todo : option pattern


        public async Task SendOTPAsync(SendMessageDTO dTO)
        {
            if (dTO.SendType == MessageType.Sms)
            {
                var service = serviceProvider.GetService<ISMSService>();
                await service.SendOTPAsync(dTO);
            }
            else
            {
                var service = serviceProvider.GetService<IEmailService>();
                await service.SendOTPAsync(dTO);
            }
        }
    }

}

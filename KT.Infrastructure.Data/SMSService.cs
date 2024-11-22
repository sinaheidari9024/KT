using KT.Application.DTOs;

namespace KT.Infrastructure.Data
{
    public class SMSService : ISMSService
    {
        private readonly IConfiguration _configuration;
        public SMSService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendOTPAsync(SendMessageDTO sendEmailDTO)
        {
            //if (sendMessageDTO is not SendSMSMessageDTO)
            //    throw new TypeMissmachtedException();
            //SendSMSMessageDTO sendEmailDTO = (SendSMSMessageDTO)sendMessageDTO;

            //TwilioClient.Init(_configuration["SmsSettings:accountSid"], _configuration["SmsSettings:authToken"]);

            //var message = await MessageResource.CreateAsync(
            //    body: $"Your OTP code is {otp}",
            //    from: new Twilio.Types.PhoneNumber("test-phone-number"),
            //    to: new Twilio.Types.PhoneNumber(MobileNumber)
            //);
        }

    }
}

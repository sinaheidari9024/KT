using KT.Infrastructure.Data;

namespace KT.Application.Commands
{
    public record SendMobileOTPCommand(string IcNumber) : IRequest;

    public class SendMobileOTPCommandValidator : AbstractValidator<SendMobileOTPCommand>
    {
        public SendMobileOTPCommandValidator()
        {
            RuleFor(c => c.IcNumber).NotEmpty().MaximumLength(15).MinimumLength(8);
        }
    }

    public class SendMobileOTPCommandHandler : IRequestHandler<SendMobileOTPCommand>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICacheService _cacheServie;
        private readonly IMessageService _messageService;

        public SendMobileOTPCommandHandler(IRepository<Customer> customerRepository, ICacheService cacheServie, IMessageService messageService)
        {
            _customerRepository = customerRepository;
            _cacheServie = cacheServie;
            _messageService = messageService;
        }

        public async Task Handle(SendMobileOTPCommand request, CancellationToken cancellationToken)
        {
            if (!_cacheServie.TryToGetData<CacheDTO>(request.IcNumber))
                throw new TryAgainException();

            var cacheResult = _cacheServie.GetData<CacheDTO>(request.IcNumber);
            if (cacheResult.State != CacheState.Init)
            {
                throw new TryAgainException();
            }

            string MobileOTP = GenerateRandomOTP();

            var message = new SendMessageDTO
            {
                SendType = MessageType.Sms,
                OTP = MobileOTP,
                MobileNumber = cacheResult.Mobile
            };

            await _messageService.SendOTPAsync(message);

            var updatedCacheData = new CacheDTO(cacheResult.Name, cacheResult.Mobile, cacheResult.CountryCode, CacheState.Init, cacheResult.Email, MobileOTP, null);
            _cacheServie.SetData(request.IcNumber, updatedCacheData, DateTimeOffset.Now.AddMinutes(5.0));

        }


        private string GenerateRandomOTP()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max).ToString();
        }
    }
}
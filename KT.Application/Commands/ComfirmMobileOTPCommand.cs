using KT.Infrastructure.Data;

namespace KT.Application.Commands
{
    public record ComfirmMobileOTPCommand(string MobileOTP, string ICNumber) : IRequest;

    public class ComfirmMobileOTPCommandValidator : AbstractValidator<ComfirmMobileOTPCommand>
    {
        public ComfirmMobileOTPCommandValidator()
        {
            RuleFor(c => c.ICNumber).NotEmpty().MaximumLength(15).MinimumLength(8);
            RuleFor(c => c.MobileOTP).NotEmpty().Length(Validation.OTPLenght);
        }
    }

    public class ComfirmMobileOTPCommandHandler : IRequestHandler<ComfirmMobileOTPCommand>
    {
        private readonly ICacheService _cacheServie;
        private readonly IMessageService _messageService;

        public ComfirmMobileOTPCommandHandler(ICacheService cacheServie, IMessageService messageService)
        {
            _cacheServie = cacheServie;
            _messageService = messageService;
        }

        public async Task Handle(ComfirmMobileOTPCommand request, CancellationToken cancellationToken)
        {
            if (!_cacheServie.TryToGetData<CacheDTO>(request.ICNumber))
                throw new TryAgainException();

            var cacheResult = _cacheServie.GetData<CacheDTO>(request.ICNumber);
            if (cacheResult.State != CacheState.Init && cacheResult.State != CacheState.PreInit)
            {
                throw new TryAgainException();
            }

            if (cacheResult.MobileOTP != request.MobileOTP)
            {
                throw new MobileOTPIncorrectException();
            }

            var emailOTP = GenerateRandomOTP();

            var message = new SendMessageDTO
            {
                SendType = MessageType.Email,
                Message = Message.EmailMessage + emailOTP,
                ToEmail = cacheResult.Email,
                Subject = Message.EmailSubject
            };
            await _messageService.SendOTPAsync(message);

            var updatedCacheData = new CacheDTO(cacheResult.Name, cacheResult.Mobile, cacheResult.CountryCode, CacheState.ComfirmMobileOTP, cacheResult.Email, null, emailOTP);
            _cacheServie.SetData(request.ICNumber, updatedCacheData, DateTimeOffset.Now.AddMinutes(5.0));

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

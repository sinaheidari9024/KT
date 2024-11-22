namespace KT.Application.Commands
{
    public record ComfirmEmailOTPCommand(string EmailOTP, string ICNumber) : IRequest;

    public class ComfirmEmailOTPCommandValidator : AbstractValidator<ComfirmEmailOTPCommand>
    {
        public ComfirmEmailOTPCommandValidator()
        {
            RuleFor(c => c.EmailOTP).NotEmpty().Length(Validation.OTPLenght);
            RuleFor(c => c.ICNumber).NotEmpty().MaximumLength(15).MinimumLength(8);
        }
    }


    public class ComfirmEmailOTPCommandHandler : IRequestHandler<ComfirmEmailOTPCommand>
    {
        private readonly ICacheService _cacheServie;

        public ComfirmEmailOTPCommandHandler(ICacheService cacheServie, IRepository<Customer> customerRepository)
        {
            _cacheServie = cacheServie;
        }

        public async Task Handle(ComfirmEmailOTPCommand request, CancellationToken cancellationToken)
        {
            if (!_cacheServie.TryToGetData<CacheDTO>(request.ICNumber))
                throw new TryAgainException();

            var cacheResult = _cacheServie.GetData<CacheDTO>(request.ICNumber);
            if (cacheResult.State != CacheState.ComfirmMobileOTP)
            {
                throw new TryAgainException();
            }

            if (cacheResult.EmailOTP != request.EmailOTP)
            {
                throw new MobileOTPIncorrectException();
            }

            var updatedCacheData = new CacheDTO(cacheResult.Name, cacheResult.Mobile, cacheResult.CountryCode, CacheState.ComfirmEmailOTP, cacheResult.Email, null, null);
            _cacheServie.SetData(request.ICNumber, updatedCacheData, DateTimeOffset.Now.AddMinutes(5.0));

        }

    }
}

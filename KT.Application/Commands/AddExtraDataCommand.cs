using KT.Application.DTOs;

namespace KT.Application.Commands
{
    public record AddExtraDataCommand(string Name, string IcNumber, string Mobile, string CountryCode, string Email) : IRequest;

    public class AddExtraDataCommandValidator : AbstractValidator<AddExtraDataCommand>
    {
        public AddExtraDataCommandValidator()
        {
            RuleFor(c => c.IcNumber).NotEmpty().MaximumLength(15).MinimumLength(8);
            RuleFor(c => c.Name).NotEmpty().MaximumLength(150).MinimumLength(8);
            RuleFor(c => c.Mobile).NotEmpty().Length(11);
            RuleFor(c => c.CountryCode).NotEmpty().MaximumLength(4);
            RuleFor(c => c.Email).NotEmpty().MaximumLength(75).EmailAddress();
        }
    }

    public class AddExtraDataCommandHandler : IRequestHandler<AddExtraDataCommand>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICacheService _cacheServie;

        public AddExtraDataCommandHandler(IRepository<Customer> customerRepository, ICacheService cacheServie)
        {
            _customerRepository = customerRepository;
            _cacheServie = cacheServie;
        }

        public async Task Handle(AddExtraDataCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(new CustomerSpec(request.Mobile, request.Email), cancellationToken);
            if (customer != null)
            {
                if (customer.MobileNo == request.Mobile)
                    throw new CustomerMobileNumberExistsException(request.Mobile);

                else if (customer.EmailAddress == request.Email)
                    throw new CustomerEmailAddressExistsException(request.Email);
            }

            if (!_cacheServie.TryToGetData<CacheDTO>(request.IcNumber))
                throw new TryAgainException();

                var cacheResult = _cacheServie.GetData<CacheDTO>(request.IcNumber);
                if (cacheResult.State != CacheState.PreInit)
                {
                    throw new TryAgainException();
                }
                
                var updatedCacheData = new CacheDTO(request.Name, request.Mobile, request.CountryCode, CacheState.Init, request.Email, null, null);
                _cacheServie.SetData(request.IcNumber, updatedCacheData, DateTimeOffset.Now.AddMinutes(5.0));

        }
    }
}
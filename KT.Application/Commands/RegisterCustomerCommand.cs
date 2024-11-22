namespace KT.Application.Commands
{
    public record RegisterCustomerCommand(string IcNumber) : IRequest<RegisterCustomerDTO>;

    public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator()
        {
            RuleFor(c => c.IcNumber).NotEmpty().MaximumLength(15).MinimumLength(8);
        }
    }

    public class RegisterCustomerCommandHandler : IRequestHandler<RegisterCustomerCommand, RegisterCustomerDTO>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICacheService _cacheServie;

        public RegisterCustomerCommandHandler(IRepository<Customer> customerRepository, ICacheService cacheServie)
        {
            _customerRepository = customerRepository;
            _cacheServie = cacheServie;
        }

        public async Task<RegisterCustomerDTO> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FirstOrDefaultAsync(new CustomerSpec(request.IcNumber), cancellationToken);
            CacheDTO cacheData;
            if (customer != null)
            {
                if (!string.IsNullOrEmpty(customer.PIN))
                    throw new CustomerICNumberExistsException(request.IcNumber);

                cacheData = new CacheDTO(customer.Name, customer.MobileNo, customer.CountryCode, CacheState.Init, customer.EmailAddress, null, null);
                _cacheServie.SetData(request.IcNumber, cacheData, DateTimeOffset.Now.AddMinutes(5.0));
                return customer.GetMaskedMobileNumber();
            }

            cacheData = new CacheDTO(null, null, null, CacheState.PreInit, null, null, null);
            _cacheServie.SetData(request.IcNumber, cacheData, DateTimeOffset.Now.AddMinutes(5.0));

            return customer.NoPinSet();
        }
    }
}

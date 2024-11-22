namespace KT.Application.Commands
{
    public record GetCustomerQuery(string IcNumber) : IRequest<RegisterCustomerDTO>;

    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(c => c.IcNumber).NotEmpty().MaximumLength(15).MinimumLength(8);
        }
    }

    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, RegisterCustomerDTO>
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly ICacheService _cacheServie;

        public GetCustomerQueryHandler(IRepository<Customer> customerRepository, ICacheService cacheServie)
        {
            _customerRepository = customerRepository;
            _cacheServie = cacheServie;
        }

        public async Task<RegisterCustomerDTO> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            CacheDTO cacheData;
            var customer = await _customerRepository.FirstOrDefaultAsync(new CustomerSpec(request.IcNumber), cancellationToken);
            if (customer != null)
            {
                if (string.IsNullOrEmpty(customer.PIN))
                {
                    cacheData = new CacheDTO(customer.Name, customer.MobileNo, customer.CountryCode, CacheState.Init, customer.EmailAddress, null, null);
                    _cacheServie.SetData(request.IcNumber, cacheData, DateTimeOffset.Now.AddMinutes(5.0));
                    return customer.GetMaskedMobileNumber();
                }
                else
                    throw new CustomerICNumberExistsException(request.IcNumber);
            }

            cacheData = new CacheDTO(null, null, null, CacheState.PreInit, null, null, null);
            _cacheServie.SetData(request.IcNumber, cacheData, DateTimeOffset.Now.AddMinutes(5.0));

            return customer.NoPinSet();
        }
    }
}

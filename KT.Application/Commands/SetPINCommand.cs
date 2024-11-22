namespace KT.Application.Commands
{
    public record SetPINCommand(string PIN, string RetypePIN, string ICNumber) : IRequest<CreateCustomerDTO>;

    public class SetPINCommandValidator : AbstractValidator<SetPINCommand>
    {
        public SetPINCommandValidator()
        {
            RuleFor(c => c.PIN).NotEmpty().Length(Validation.PINLenght);
            RuleFor(c => c.RetypePIN).NotEmpty().Length(Validation.PINLenght);
            RuleFor(c => c.ICNumber).NotEmpty().MaximumLength(15).MinimumLength(8);
        }
    }


    public class SetPINCommandHandler : IRequestHandler<SetPINCommand, CreateCustomerDTO>
    {
        private readonly ICacheService _cacheServie;
        private readonly IRepository<Customer> _customerRepository;

        public SetPINCommandHandler(ICacheService cacheServie, IRepository<Customer> customerRepository)
        {
            _cacheServie = cacheServie;
            _customerRepository = customerRepository;
        }

        public async Task<CreateCustomerDTO> Handle(SetPINCommand request, CancellationToken cancellationToken)
        {
            if (request.PIN != request.RetypePIN)
            {
                throw new PinsNotSameException();
            }

            if (!_cacheServie.TryToGetData<CacheDTO>(request.ICNumber))
                throw new TryAgainException();

            var cacheResult = _cacheServie.GetData<CacheDTO>(request.ICNumber);
            if (cacheResult.State != CacheState.ComfirmEmailOTP)
                throw new TryAgainException();

            var customer = await _customerRepository.FirstOrDefaultAsync(new CustomerSpec(request.ICNumber), cancellationToken);
            if (customer != null)
            {
                customer.SetPin(request.PIN);
                await _customerRepository.UpdateAsync(customer, cancellationToken);
                await _customerRepository.SaveChangesAsync(cancellationToken);
                return customer.Map();
            }
            else
            {
                var newCustomer = new Customer(cacheResult.Name, request.ICNumber, cacheResult.Email, cacheResult.Mobile, cacheResult.CountryCode, request.PIN);
                await _customerRepository.AddAsync(newCustomer, cancellationToken);
                await _customerRepository.SaveChangesAsync(cancellationToken);
                return newCustomer.Map();
            }

        }
    }
}

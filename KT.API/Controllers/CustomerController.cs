namespace KT.API.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{icNumber}")]
        [ProducesResponseType(typeof(RegisterCustomerDTO), StatusCodes.Status200OK)]
        public Task<RegisterCustomerDTO> GetCustomerAsync([FromRoute] string icNumber, CancellationToken cancellationToken)
        {
            return _mediator.Send(new GetCustomerQuery(icNumber), cancellationToken);
        }

        [HttpPost("addExtraData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task AddExtraDataAsync([FromBody] AddExtraDataCommand addExtraDataCommand, CancellationToken cancellationToken)
        {
            return _mediator.Send(addExtraDataCommand, cancellationToken);
        }

        [HttpPost("sendMobileOTP")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task AddExtraDataAsync([FromBody] SendMobileOTPCommand sendMobileOTPCommand, CancellationToken cancellationToken)
        {
            return _mediator.Send(sendMobileOTPCommand, cancellationToken);
        }

        [HttpPost("comfirmMobileOTP")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task ComfirmMobileOTPAsync([FromBody] ComfirmMobileOTPCommand comfirmMobileOTPCommand, CancellationToken cancellationToken)
        {
            return _mediator.Send(comfirmMobileOTPCommand, cancellationToken);
        }

        [HttpPost("comfirmEmailOTP")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public Task ComfirmEmailOTP([FromBody] ComfirmEmailOTPCommand comfirmEmailOTPCommand, CancellationToken cancellationToken)
        {
            return _mediator.Send(comfirmEmailOTPCommand, cancellationToken);
        }

        [HttpPost("setPIN")]
        [ProducesResponseType(typeof(CreateCustomerDTO), StatusCodes.Status200OK)]
        public Task<CreateCustomerDTO> SetPINCommandAsync([FromBody] SetPINCommand setPINCommand, CancellationToken cancellationToken)
        {
            return _mediator.Send(setPINCommand, cancellationToken);
        }
    }
}
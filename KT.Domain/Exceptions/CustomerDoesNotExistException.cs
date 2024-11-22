namespace KT.Domain.Exceptions
{
    public class CustomerDoesNotExistException : KTException
    {
        public CustomerDoesNotExistException(int customerId) :
            base($"a customer with id {customerId} doesn't exist")
        { }
    }
}

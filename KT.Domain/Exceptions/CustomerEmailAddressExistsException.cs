namespace KT.Domain.Exceptions
{
    public class CustomerEmailAddressExistsException : KTException
    {
        public CustomerEmailAddressExistsException(string emailAddress) :
            base($"a customer exists with email address {emailAddress}")
        { }
    }
}
